using Microsoft.Extensions.Configuration;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Text;

namespace FavoDeMel.Domain.Core.Model.Configuration
{
    public class ElasticSettings : SettingsBase
    {
        public string DeadLetterIndexName => $"dead-letter-{Uri}-{0:yyyy.MM.dd}";
        public LogEventLevel? MinimumLogEventLevel => LogEventLevel.Information;
        public AutoRegisterTemplateVersion AutoRegisterTemplateVersion => AutoRegisterTemplateVersion.ESv6;
        public int NumberOfShards => 3;
        public int? NumberOfReplicas => 0;
        public bool AutoRegisterTemplate => true;
        public bool OverwriteTemplate => true;
        public TimeSpan Period => TimeSpan.FromSeconds(60);
        public int BatchPostingLimit => 500;

        public readonly Uri Uri;
        public readonly string IndexFormat;


        public ElasticSettings(IConfiguration configuration)
        {
            Uri = new Uri(configuration[AppSettings.Keys.ElasticSearch_Logs.URI]);
            IndexFormat = configuration[AppSettings.Keys.ElasticSearch_Logs.INDEXFORMAT];
        }

        public override string ToString()
        {
            var strB = new StringBuilder();

            strB.AppendLine(MontarTextoChaveValor(nameof(Uri), Uri.ToString()));
            strB.AppendLine(MontarTextoChaveValor(nameof(IndexFormat), IndexFormat));

            return strB.ToString();
        }
    }
}