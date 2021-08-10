using System.Text;

namespace FavoDeMel.Domain.Core.Model.Configuration
{
    public class AppSettings : SettingsBase
    {
        #region Keys

        public static class Keys
        {
            public static class ElasticSearch_Logs
            {
                public const string URI = "ELASTICSEARCH_LOGS:URI";
                public const string INDEXFORMAT = "ELASTICSEARCH_LOGS:INDEXFORMAT";
            }

            public static class SSO
            {
                public const string AUTHORITY_URI = "IDENTITYPROVIDER:AUTHORITYURI";
                public const string SCOPES = "IDENTITYPROVIDER:SCOPES";
            }

            public static class ConnectionStrings
            {
                public const string Catalogo_CONNECTION_STRING = "CatalogoDbConnection";
                public const string Venda_CONNECTION_STRING = "VendaDbConnection";
                public const string DEFAULT_EVENT_STORE_CONNECTION_STRING = "EventStoreConnection";
                public const string MEMORY_LIMIT = "MEMORY-LIMIT";
            }

            public static class Swagger
            {
                public const string TITLE = "Swagger:App:Title";
                public const string DESCRIPTION = "Swagger:App:Description";
                public const string VERSION = "Swagger:App:Version";

                public static class Autenticacao
                {
                    public const string DESCRIPTION = "Swagger:Autenticacao:Description";
                    public const string NAME = "Swagger:Autenticacao:Name";
                    public const string IN = "Swagger:Autenticacao:In";
                    public const string TYPE = "Swagger:Autenticacao:Type";
                }
            }

        }

        #endregion Keys
        public IdentityProvider IdentityProvider { get; set; }
        public DataSettings Data { get; set; }
        public SwaggerSettings Swagger { get; set; }
        public ElasticSettings ElasticLogs { get; set; }
        public RabbitMqSettings RabbitMqSettings { get; set; }

        public override string ToString()
        {
            var strB = new StringBuilder();
            strB.AppendLine(IdentityProvider.ToString());
            strB.AppendLine(Data.ToString());
            strB.AppendLine(ElasticLogs.ToString());
            strB.AppendLine(RabbitMqSettings.ToString());

            return strB.ToString();
        }
    }
}
