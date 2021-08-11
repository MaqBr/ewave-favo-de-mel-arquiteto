using BuildingBlocks.EventBus;
using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.EventBusRabbitMQ;
using MediatR;
using FavoDeMel.Catalogo.Api.Factories;
using FavoDeMel.Catalogo.Data.EF.Context;
using FavoDeMel.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;
using FavoDeMel.Domain.Core.Model.Configuration;
using FavoDeMel.Domain.Core.Extensions;
using FavoDeMel.Catalogo.Application.AutoMapper;
using System.Collections.Generic;
using FavoDeMel.Catalogo.Data.Dapper.Abstractions;
using System;
using FavoDeMel.Catalogo.Application.Queries;
using System.Linq;
using FavoDeMel.Catalogo.Application.ViewModels;
using FavoDeMel.Catalogo.Domain;
using FavoDeMel.Catalogo.Application.Services;
using FavoDeMel.Catalogo.Data.EF.Repository;
using FavoDeMel.Catalogo.Application.Interfaces;
using FavoDeMel.Catalogo.Data.Dapper.Connection;
using FavoDeMel.Venda.Application.Events;
using FavoDeMel.Catalogo.Domain.Events;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;

namespace FavoDeMel.Catalogo.Api
{
    public class Startup
    {
        private readonly AppSettings _appSettings;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _appSettings = configuration.GetAppSettings();
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            var logger = SerilogFactory.GetLogger(_appSettings.ElasticLogs);
            services.Configure<AppSettings>(c =>
            {
                c.Data = _appSettings.Data;
                c.IdentityProvider = _appSettings.IdentityProvider;
                c.Swagger = _appSettings.Swagger;
            });

            services.AddDbContext<CatalogoDbContext>(options =>
            {
                options.UseSqlServer(_appSettings.Data.CatalogoConnection);
            });

            services
                 .AddLogging(loggingBuilder =>
                 {
                     loggingBuilder
                     .ClearProviders()
                     .AddConsole()
                     .AddSerilog(logger, dispose: true);
                 });
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
            services.AddMediatR(typeof(Startup));
            RegisterServices(services);

            services.AddEventBus(_appSettings.RabbitMqSettings);
            services.ConfigureRabbitMQ(_appSettings.RabbitMqSettings);
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer",
                    options =>
                    {
                        options.Authority = _appSettings.IdentityProvider.AuthorityUri;
                        options.Audience = _appSettings.IdentityProvider.Scopes[0];
                    });

            services.ConfigureSwagger(_appSettings.Swagger);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalogo API V1");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(DocExpansion.List);
            });

            app.UseHttpsRedirection();

            app.UseRouting()
            .UseWebSockets()
            .UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            ConfigureAuth(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        protected virtual void ConfigureAuth(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

        private void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);

            //Application Services
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();

            //Data
            services.AddScoped<CatalogoDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            //Finders
            services.AddTransient<IBackendConnectionFactory, BackendConnectionFactory>();

            foreach (var item in GetClassName("Finder"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }

            //Notifications
            services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoIniciadoEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoProcessamentoCanceladoEvent>, ProdutoEventHandler>();

            //Queries Commands
            services.AddTransient<IRequestHandler<ObterTodosProdutosQuery, IEnumerable<ProdutoViewModel>>, ObterTodosProdutosQueryHandler>();

        }

        private static Dictionary<Type, Type[]> GetClassName(string assemblyName)
        {
            if (string.IsNullOrEmpty(assemblyName))
                return new Dictionary<Type, Type[]>();

            Type type = typeof(FinderSql);

            var assembly = type.Assembly;
            var ts = assembly.GetTypes().ToList();

            var result = new Dictionary<Type, Type[]>();

            foreach (var item in ts.Where(s => !s.IsInterface))
            {
                var isIsAssignable = type.IsAssignableFrom(item);

                if (isIsAssignable)
                {
                    var interfaceType = item.GetInterfaces();
                    result.Add(item, interfaceType);
                }
            }
            return result;
        }


    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, RabbitMqSettings settings)
        {
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = settings.Queue;
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<IServiceScopeFactory>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                var retryCount = settings.Retry;

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IServiceCollection ConfigureRabbitMQ(this IServiceCollection services, RabbitMqSettings settings)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = settings.Url,
                    VirtualHost = settings.VirtualHost,
                    UserName = settings.Usuario,
                    Password = settings.Senha,
                    DispatchConsumersAsync = true
                };

                var retryCount = 5;

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, SwaggerSettings settings)
        {
            services.AddSwaggerGen(c =>
            {
                
                c.SwaggerDoc("v1", new OpenApiInfo { Title = settings.Title, Version = settings.Version });
                // add JWT Authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = settings.Autenticacao.Name,
                    Description = settings.Autenticacao.Description,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });

            });

            return services;
        }
     }
}
