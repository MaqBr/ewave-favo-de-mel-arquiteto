using Microsoft.Extensions.Configuration;
using System.Text;

namespace FavoDeMel.Domain.Core.Model.Configuration
{
    public class RabbitMqSettings : SettingsBase
    {
        public string Url { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string VirtualHost { get; set; }
        public string Queue { get; set; }
        public int Retry { get; set; }

        public RabbitMqSettings(IConfiguration configuration)
        {
            Url = configuration["RABBITMQ:URL"];
            Usuario = configuration["RABBITMQ:USUARIO"];
            Senha = configuration["RABBITMQ:SENHA"];
            VirtualHost = configuration["RABBITMQ:VHOST"];
            Queue = configuration["RABBITMQ:QUEUE"];
            Retry = int.Parse(configuration["RABBITMQ:RETRY"]);
        }

        public override string ToString()
        {
            var strB = new StringBuilder();
            strB.AppendLine($"____{nameof(RabbitMqSettings)}____");
            strB.AppendLine($" Localhost: {Url}");
            strB.AppendLine(" ");
            strB.AppendLine($" Usuário: {Usuario}");
            strB.AppendLine(" ");
            strB.AppendLine($" Senha: {Senha}");
            strB.AppendLine(" ");
            strB.AppendLine($" VirtualHost: {VirtualHost}");
            strB.AppendLine(" ");
            strB.AppendLine("________________________________________");
            return strB.ToString();
        }
    }
}
