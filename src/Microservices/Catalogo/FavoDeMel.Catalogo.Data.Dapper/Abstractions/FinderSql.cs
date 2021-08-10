using FavoDeMel.Catalogo.Application.Interfaces;
using System;

namespace FavoDeMel.Catalogo.Data.Dapper.Abstractions
{
    public abstract class FinderSql : Finder
    {
        protected IConnectionFactory ConnectionFactory;

        protected FinderSql(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory ?? throw new ArgumentException(nameof(connectionFactory));
        }
    }
}