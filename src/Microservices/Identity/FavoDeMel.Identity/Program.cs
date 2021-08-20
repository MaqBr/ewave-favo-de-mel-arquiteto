using Microsoft.Extensions.DependencyInjection;
using FavoDeMel.Identity;
using FavoDeMel.Identity.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Logging;
using FavoDeMel.Identity.Infrastructure;

var configuration = GetConfiguration();

try
{

    var host = CreateHostBuilder(configuration, args);

    host.MigrateDbContext<ApplicationDbContext>((context, services) =>
    {
        var env = services.GetService<IWebHostEnvironment>();
        var logger = services.GetService<ILogger<ApplicationDbContextSeed>>();

        new ApplicationDbContextSeed()
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
