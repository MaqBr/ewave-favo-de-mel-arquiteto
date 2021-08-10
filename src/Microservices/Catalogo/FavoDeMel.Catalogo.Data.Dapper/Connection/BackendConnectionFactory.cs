using FavoDeMel.Catalogo.Application.Interfaces;
using FavoDeMel.Domain.Core.Model.Configuration;
using Microsoft.Extensions.Configuration;

namespace FavoDeMel.Catalogo.Data.Dapper.Connection
{
    public class BackendConnectionFactory : SqlConnectionFactory, IBackendConnectionFactory
    {
        public BackendConnectionFactory(IConfiguration configuration) 
            : base(configuration[AppSettings.Keys.ConnectionStrings.Catalogo_CONNECTION_STRING])
        {
            
        }
    }
}