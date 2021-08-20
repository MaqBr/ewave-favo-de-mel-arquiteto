using FavoDeMel.Catalogo.Api.Infrastructure;
using FavoDeMel.Venda.Api;
using FavoDeMel.Venda.Data.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

var configuration = GetConfiguration();

try
{

    var host = CreateHostBuilder(configuration, args);

    host.MigrateDbContext<VendaDbContext>((context, services) =>
    {
        var env = services.GetService<IWebHostEnvironment>();
        var logger = services.GetService<ILogger<VendaContextSeed>>();

        new VendaContextSeed()
                .SeedAsync(context, env, logger)
                .Wait();
    });

    host.Run();
    return 0;
}
catch (Exception ex)
{
    return 1;
}

IWebHost CreateHostBuilder(IConfiguration configuration, string[] args) =>
  WebHost.CreateDefaultBuilder(args)
      .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
      .CaptureStartupErrors(false).UseStartup<Startup>()
                   .Build();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    var config = builder.Build();
    return builder.Build();
}

public static class Program
{
    public static string Namespace = typeof(Startup).Namespace;
    public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}
