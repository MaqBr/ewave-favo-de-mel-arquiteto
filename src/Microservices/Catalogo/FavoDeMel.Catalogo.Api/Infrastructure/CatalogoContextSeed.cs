using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using FavoDeMel.Catalogo.Data.EF.Context;
using System.Collections.Generic;
using FavoDeMel.Catalogo.Domain;
using System.Linq;

namespace FavoDeMel.Catalogo.Api.Infrastructure
{
    public class CatalogoContextSeed
    {
        public async Task SeedAsync(CatalogoDbContext context, IWebHostEnvironment env, ILogger<CatalogoContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(CatalogoContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                if (!context.Categorias.Any())
                {
                    await context.Categorias.AddRangeAsync(ObterSeedCategorias());
                    await context.SaveChangesAsync();

                    await context.Produtos.AddRangeAsync(ObterSeedProdutos(context.Categorias.ToList()));
                    await context.SaveChangesAsync();
                }
            });
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<CatalogoContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }


        private IEnumerable<Categoria> ObterSeedCategorias()
        {
            return new List<Categoria>()
            {
                new Categoria("Massas", 102),
                new Categoria("Bebidas", 101)
            };
        }

        private IEnumerable<Produto> ObterSeedProdutos(List<Categoria> categorias)
        {
            return new List<Produto>()
            {
                new Produto("Talharim (Nero Di Seppia - Tinta de Lula)", "Talharim (Nero Di Seppia - Tinta de Lula)", true, 20, categorias[0].Id, DateTime.Now, "produto-talharim-tinta-lula.png", null, 100),
                new Produto("Nhoque de Mandioquinha", "Nhoque de Mandioquinha", true, 20, categorias[0].Id, DateTime.Now, "produto-nhoque-mandioquinha.png", null, 100),
                new Produto("Parpadelle com tomatinho", "Parpadelle com tomatinho", true, 20, categorias[0].Id, DateTime.Now, "produto-parpadelle-com-tomatinho.png", null, 100),
                new Produto("Nhoque de Mandioquinha sem farinha", "Nhoque de Mandioquinha sem farinha", true, 20, categorias[0].Id, DateTime.Now, "produto-nhoque-mandioquinha2.png", null, 100),
            };
        }
    }
}
