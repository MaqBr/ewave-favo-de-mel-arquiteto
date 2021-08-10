using FavoDeMel.Domain.Core.Model.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace FavoDeMel.Venda.Api.Factories
{
    public static class SerilogFactory
    {
        public static ILogger GetLogger(ElasticSettings elasticConfiguration)
        {
            /*descrição das configurações: 
             https://github.com/serilog/serilog-sinks-elasticsearch/wiki/Configure-the-sink */

            /*Verbose - tracing information and debugging minutiae; generally only switched on in unusual situations
            Debug - internal control flow and diagnostic state dumps to facilitate pinpointing of recognised problems
            Information - events of interest or that have relevance to outside observers; the default enabled minimum logging level
            Warning - indicators of possible issues or service/functionality degradation
            Error - indicating a failure within the application or connected system
            Fatal - critical errors causing complete failure of the application
            */

            //todo: https://github.com/serilog/serilog-aspnetcore
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Default", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.AspNetCore.Session.CookieProtection", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.AspNetCore.DataProtection.KeyManagement.KeyRingBasedDataProtector", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Error)
                .MinimumLevel.Override("IdentityServer4.Hosting.IdentityServerMiddleware", LogEventLevel.Error)
                .MinimumLevel.Override("IdentityServer4.Endpoints.AuthorizeEndpoint", LogEventLevel.Error)
                .MinimumLevel.Override("Jaeger.Reporters", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                //.Enrich.WithMachineName()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticConfiguration.Uri)
                {
                    AutoRegisterTemplate = elasticConfiguration.AutoRegisterTemplate,
                    OverwriteTemplate = elasticConfiguration.OverwriteTemplate,
                    MinimumLogEventLevel = elasticConfiguration.MinimumLogEventLevel,
                    Period = elasticConfiguration.Period,
                    BatchPostingLimit = elasticConfiguration.BatchPostingLimit,
                    AutoRegisterTemplateVersion = elasticConfiguration.AutoRegisterTemplateVersion,
                    IndexFormat = elasticConfiguration.IndexFormat,
                    NumberOfShards = elasticConfiguration.NumberOfShards,
                    NumberOfReplicas = elasticConfiguration.NumberOfReplicas,
                    DeadLetterIndexName = elasticConfiguration.DeadLetterIndexName,
                    CustomFormatter = new ElasticsearchJsonFormatter()
                })
                .WriteTo.Console()
            .CreateLogger();
        }
    }
}
