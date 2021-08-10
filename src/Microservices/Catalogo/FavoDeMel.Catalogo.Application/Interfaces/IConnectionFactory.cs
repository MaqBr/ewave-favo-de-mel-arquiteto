using System;
using System.Data;

namespace FavoDeMel.Catalogo.Application.Interfaces
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection Create();
    }
}