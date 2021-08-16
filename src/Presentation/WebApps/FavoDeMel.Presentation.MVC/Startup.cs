using BuildingBlocks.EventSourcing;
using FavoDeMel.Domain.Core.CommonMessages.Notifications;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Domain.Core.Data.EventSourcing;
using FavoDeMel.Domain.Core.Extensions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.Notifications;
using FavoDeMel.Domain.Core.Model.Configuration;
using FavoDeMel.Presentation.MVC.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using WebMVC.Infrastructure;

namespace FavoDeMel.Presentation.MVC
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
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<AppSettings>(c =>
            {
                c.IdentityProvider = _appSettings.IdentityProvider;
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddHttpClientServices(Configuration);
            services.AddCustomAuthentication(_appSettings.IdentityProvider);
            services.AddHttpClient();
            services.AddControllers();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/conta/entrar");
            });
            services.AddMediatR(typeof(Startup));
            // Event Sourcing
            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }

    static class ServiceCollectionExtensions
    {
        // Adds all Http client services
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //set 5 min as the lifetime for each HttpMessageHandler int the pool
            services.AddHttpClient("extendedhandlerlifetime")
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            //add http client services
            services.AddHttpClient<IProdutoAppService, ProdutoAppService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));
            services.AddHttpClient<IPedidoAppService, PedidoAppService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            return services;
        }


        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IdentityProvider identityProvider)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            IdentityModelEventSource.ShowPII = true;//Somente em ambiente de desenvolvimento

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = identityProvider.AuthorityUri;
                    options.ClientId = "715000d0c10040258c1be259c09e3b91";
                    options.ClientSecret = "360ceac2e80545dca6083fef4f94d09f";
                    options.ResponseType = "code";
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("api_favo_mel");
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role",

                    };
                    //options.MetadataAddress = $"{identityProvider.AuthorityUri}/.well-known/openid-configuration";
                    options.RequireHttpsMetadata = false;
                });

            return services;
        }
    }
}
