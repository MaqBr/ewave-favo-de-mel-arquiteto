using HealthChecks.UI.Client;
using FavoDeMel.WebStatus.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using FavoDeMel.Domain.Core.Model.Configuration;
using FavoDeMel.Domain.Core.Extensions;

namespace FavoDeMel.WebStatus
{
    public class Startup
    {
        private readonly AppSettings _appSettings;
        public Startup(IConfiguration configuration)
        {
            _appSettings = configuration.GetAppSettings();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(c =>
            {
                c.Data = _appSettings.Data;
                c.RabbitMqSettings = _appSettings.RabbitMqSettings;
                c.ElasticLogs = _appSettings.ElasticLogs;
            });

            services.AddControllers();
            services.AddCustomHealthChecks(_appSettings.Data, _appSettings.RabbitMqSettings);
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
            }

            app.UseRouting()
                .UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseHealthChecks("/healthz", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

        }

    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, DataSettings dataSettings, RabbitMqSettings rabbitMqSettings)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder
                .AddCheck("SqlServer - Catálogo",
                    new SqlServerCatalogoDbHealthCheck(dataSettings.CatalogoConnection))
                .AddCheck("SqlServer - Catálogo",
                    new SqlServerVendaDbHealthCheck(dataSettings.VendaConnection))
                .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "essential" })
                .AddProcessAllocatedMemoryHealthCheck(dataSettings.MemoryLimit, "memory", tags: new[] { "essential" })
                .AddRabbitMQ(
                    $"amqp://{rabbitMqSettings.Usuario}:{rabbitMqSettings.Senha}@{rabbitMqSettings.Url}:5672/{rabbitMqSettings.VirtualHost}",
                    name: "rabbitmqbus-check",
                    tags: new string[] { "rabbitmqbus" });

            return services;

        }

    }
}
