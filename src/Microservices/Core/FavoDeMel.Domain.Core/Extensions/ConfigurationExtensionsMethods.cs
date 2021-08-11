using FavoDeMel.Domain.Core.Model.Configuration;
using Microsoft.Extensions.Configuration;
using System;

namespace FavoDeMel.Domain.Core.Extensions
{
    public static class ConfigurationExtensionsMethods
    {
        public static AppSettings GetAppSettings(this IConfiguration configuration)
        {
            var settings = new AppSettings
            {
                IdentityProvider = new IdentityProvider(configuration),
                Data = new DataSettings(configuration),
                Microservices = new MicroServiceSettings(configuration),
                ElasticLogs = new ElasticSettings(configuration),
                RabbitMqSettings = new RabbitMqSettings(configuration),

                Swagger = new SwaggerSettings()
                {
                    Title = configuration[AppSettings.Keys.Swagger.TITLE],
                    Description = configuration[AppSettings.Keys.Swagger.DESCRIPTION],
                    Version = configuration[AppSettings.Keys.Swagger.VERSION],

                    Autenticacao = new SwaggerAutenticacaoSettings()
                    {
                        Description = configuration[AppSettings.Keys.Swagger.Autenticacao.DESCRIPTION],
                        Name = configuration[AppSettings.Keys.Swagger.Autenticacao.NAME]
                    }
                },
            };

            Console.WriteLine(settings.ToString());

            return settings;
        }
    }
}
