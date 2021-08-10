using FavoDeMel.Catalogo.Application.Interfaces;
using FavoDeMel.Catalogo.Data.Dapper.Connection;

namespace FavoDeMel.Catalogo.Data.Dapper.ExtensionsMethods
{
    public static class IConnectionFactoryExtensionsMethods
    {
        public static ConnectionStateManager CreateManaged(this IConnectionFactory source)
        {
            return new ConnectionStateManager(source.Create());
        }
    }
}
