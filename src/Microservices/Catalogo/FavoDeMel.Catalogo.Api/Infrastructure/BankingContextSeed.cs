using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using FavoDeMel.Catalogo.Data.EF.Context;

namespace FavoDeMel.Catalogo.Api.Infrastructure
{
    public class CatalogoContextSeed
    {
        public async Task SeedAsync(CatalogoDbContext context, IWebHostEnvironment env, ILogger<CatalogoContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(CatalogoContextSeed));

            await policy.ExecuteAsync(async () =>
            {

                var contentRootPath = env.ContentRootPath;


                using (context)
                {
                    context.Database.Migrate();

                    //TODO:

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

    }
}
