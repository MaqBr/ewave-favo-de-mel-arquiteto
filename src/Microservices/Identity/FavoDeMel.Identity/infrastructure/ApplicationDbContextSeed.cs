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
using FavoDeMel.Identity.Data;
using FavoDeMel.Identity.Model;

namespace FavoDeMel.Catalogo.Api.Infrastructure
{
    public class ApplicationDbContextSeed
    {
        public async Task SeedAsync(ApplicationDbContext context, IWebHostEnvironment env, ILogger<ApplicationDbContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(ApplicationDbContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                if (!context.Users.Any())
                {
                    await context.Users.AddRangeAsync(ObterUsuarios());
                    await context.SaveChangesAsync();
                }
            });
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<ApplicationDbContextSeed> logger, string prefix, int retries = 3)
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

        private IEnumerable<ApplicationUser> ObterUsuarios()
        {
            var garcom =
            new ApplicationUser()
            {
                Id = "53d600db-27df-4de2-b785-689000a87802",
                Name = "DemoUser [Garçom]",
                UserName = "garcom@teste.com",
                NormalizedEmail = "GARCOM@TESTE.COM",
                NormalizedUserName = "garcom@teste.com",
                Email = "garcom@teste.com",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAENVCDwvhNyXVpe+xLwOrWwW4vDLl66BXkLBDiT3cJttN6yg2SGdY/EZP2qr6Nu5yzA==",
                SecurityStamp = "CP3ZEXWSIJSG5IWUFRTKEIQ46ZS32UUR",
                ConcurrencyStamp = "e86ba905-84b9-4abb-9fe1-5ee252238d2b",
                LockoutEnabled = false
            };

            var cozinheiro =
                new ApplicationUser()
                {
                    Id = "d952c834-8802-47ba-bf40-6c197d74dad0",
                    Name = "DemoUser [Cozinheiro]",
                    UserName = "cozinha@teste.com",
                    NormalizedEmail = "COZINHA@TESTE.COM",
                    NormalizedUserName = "cozinha@teste.com",
                    Email = "COZINHA@TESTE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEDSfZ8W5PMEb8f37rmfoa9Zco2N0krQ1Djk2cUNFEPfsa1nRv/z/oxHZU6MPQmXhAg==",
                    SecurityStamp = "WU3CB5IVVHKLBW2MFPREVYDDC3MZRUBX",
                    ConcurrencyStamp = "62dd00c3-9015-4981-90c9-8b83f8c17cc3",
                    LockoutEnabled = false
                };

            return new List<ApplicationUser> { garcom, cozinheiro };
        }
    }
}
