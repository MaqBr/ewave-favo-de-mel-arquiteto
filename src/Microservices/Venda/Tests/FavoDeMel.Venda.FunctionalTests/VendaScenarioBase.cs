using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FavoDeMel.Venda.Api;
using System;
using FavoDeMel.Venda.Api.Infrastructure;
using FavoDeMel.Venda.Data.Context;

namespace Venda.FunctionalTests
{
    public class VendaScenarioBase
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(VendaScenarioBase))
                .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                }).UseStartup<Startup>();

            var testServer = new TestServer(hostBuilder);

            testServer.Host
                .MigrateDbContext<VendaDbContext>((context, services) =>
                {
                    var env = services.GetService<IWebHostEnvironment>();
                    var logger = services.GetService<ILogger<VendaContextSeed>>();

                    new VendaContextSeed()
                        .SeedAsync(context, env, logger)
                        .Wait();
                });

            return testServer;
        }

        public static class Get
        {
            public static string ObterMesas()
            {
                return "api/mesa/obter/todos";
            }

        }

    }
}
