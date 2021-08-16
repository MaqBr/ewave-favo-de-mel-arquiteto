using FavoDeMel.IdentityExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using FavoDeMel.Identity.Businnes;

namespace FavoDeMel.IdentityConfiguration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}