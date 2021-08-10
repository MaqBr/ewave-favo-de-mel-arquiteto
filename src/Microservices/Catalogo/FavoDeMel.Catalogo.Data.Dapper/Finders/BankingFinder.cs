using FavoDeMel.Catalogo.Data.Dapper.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Catalogo.Data.Dapper.ExtensionsMethods;
using Dapper;
using System.Data;
using System;
using FavoDeMel.Catalogo.Application.Interfaces;
using FavoDeMel.Catalogo.Domain.Models;

namespace FavoDeMel.Catalogo.Data.Dapper.Finders
{
    public class CatalogoFinder : FinderSql, ICatalogoFinder
    {
        public CatalogoFinder(IBackendConnectionFactory connection) : base(connection)
        {
        }

        public async Task<IEnumerable<CatalogoDTO>> GetAll()
        {
            try
            {
                using var cnn = ConnectionFactory.CreateManaged();
                var data = await cnn.QueryAsync<CatalogoDTO>($"select * from Accounts");
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter os dados", ex);
            }
        }


    }
}