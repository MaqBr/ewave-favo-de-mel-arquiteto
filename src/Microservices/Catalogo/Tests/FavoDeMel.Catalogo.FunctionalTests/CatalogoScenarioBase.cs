using FavoDeMel.Catalogo.Data.EF.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FavoDeMel.Catalogo.Api.Infrastructure;
using FavoDeMel.Catalogo.Api;

namespace Catalogo.FunctionalTests
{
    public class CatalogoScenarioBase
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(CatalogoScenarioBase))
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
                .MigrateDbContext<CatalogoDbContext>((context, services) =>
                {
                    var env = services.GetService<IWebHostEnvironment>();
                    var logger = services.GetService<ILogger<CatalogoContextSeed>>();

                    new CatalogoContextSeed()
                        .SeedAsync(context, env, logger)
                        .Wait();
                });

            return testServer;
        }

        public static class Get
        {
            public static string ObterProdutos = "api/produto/vitrine";

            public static string ProdutoDetalhe(int id)
            {
                return $"api/produto/produto-detalhe/{id}";
            }
        }

    }
}
