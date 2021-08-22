using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;
using FavoDeMel.Venda.Data.Context;
using FavoDeMel.Venda.Domain.Models;

namespace FavoDeMel.Venda.Api.Infrastructure
{
    public class VendaContextSeed
    {
        public async Task SeedAsync(VendaDbContext context, IWebHostEnvironment env, ILogger<VendaContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(VendaContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                if (!context.Mesas.Any())
                {
                    await context.Mesas.AddRangeAsync(ObterSeedMesas());
                    await context.SaveChangesAsync();
                }
            });
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<VendaContextSeed> logger, string prefix, int retries = 10)
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


        private IEnumerable<Mesa> ObterSeedMesas()
        {
            return new List<Mesa>()
            {
                new Mesa { Numero = 1, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new Mesa { Numero = 2, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new Mesa { Numero = 3, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new Mesa { Numero = 4, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new Mesa { Numero = 5, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new Mesa { Numero = 6, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new Mesa { Numero = 7, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new Mesa { Numero = 8, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new Mesa { Numero = 9, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new Mesa { Numero = 10, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre }
            };
        }
    }
}
