using BuildingBlocks.EventBus;
using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.EventBusRabbitMQ;
using MediatR;
using FavoDeMel.Venda.Api.Factories;
using FavoDeMel.Venda.Data.Context;
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
using System.Text.Json.Serialization;
using FavoDeMel.Domain.Core.Model.Configuration;
using FavoDeMel.Domain.Core.Extensions;
using FavoDeMel.Infra.IoC;
using FavoDeMel.Catalogo.Application.AutoMapper;
using FavoDeMel.Venda.Application;
using FavoDeMel.Venda.Application.Queries;
using FavoDeMel.Venda.Domain.Models;
using FavoDeMel.Venda.Data.Repository;
using FavoDeMel.Venda.Application.Events;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

namespace FavoDeMel.Venda.Api
{
    public class Startup
    {
        private readonly AppSettings _appSettings;

        public Startup(IConfiguration configuration)
        {
            _appSettings = configuration.GetAppSettings();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var logger = SerilogFactory.GetLogger(_appSettings.ElasticLogs);
            services.Configure<AppSettings>(c =>
            {
                c.Data = _appSettings.Data;
                c.IdentityProvider = _appSettings.IdentityProvider;
                c.Swagger = _appSettings.Swagger;
            });

            services.AddDbContext<VendaDbContext>(options =>
            {
                options.UseSqlServer(_appSettings.Data.VendaConnection);
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
            services.AddEventBus(_appSettings.RabbitMqSettings);
            services.ConfigureRabbitMQ(_appSettings.RabbitMqSettings);

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            RegisterServices(services);

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer",
                    options =>
                    {
                        options.Authority = _appSettings.IdentityProvider.AuthorityUri;
                        options.Audience = _appSettings.IdentityProvider.Scopes[0];
                        options.RequireHttpsMetadata = false;
                    });
            services.AddHealthChecks();
            services.ConfigureSwagger(_appSettings.Swagger);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Venda API V1");
                c.RoutePrefix = string.Empty;
            });

            ConfigureEventBus(app);

            app.UseHttpsRedirection();

            app.UseRouting()
            .UseWebSockets()
            .UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHealthChecks("/healthz", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            ConfigureEventBus(app);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);

            services.AddScoped<IComandaRepository, ComandaRepository>();
            services.AddScoped<IComandaQueries, ComandaQueries>();
            services.AddScoped<VendaDbContext>();

            services.AddScoped<IRequestHandler<AdicionarItemComandaCommand, bool>, ComandaCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarItemComandaCommand, bool>, ComandaCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverItemComandaCommand, bool>, ComandaCommandHandler>();
            services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, ComandaCommandHandler>();
            services.AddScoped<IRequestHandler<IniciarComandaCommand, bool>, ComandaCommandHandler>();
            services.AddScoped<IRequestHandler<FinalizarComandaCommand, bool>, ComandaCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarComandaCommand, bool>, ComandaCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarProcessamentoComandaCommand, bool>, ComandaCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarProcessamentoComandaEstornarEstoqueCommand, bool>, ComandaCommandHandler>();

        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            //Exemplo de registro de subscriber
            //eventBus.Subscribe<PedidoProdutoAdicionadoEvent, PedidoEventHandler>();
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
            //Exemplo de registro de subscriber
            //services.AddTransient<PedidoEventHandler>();

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
