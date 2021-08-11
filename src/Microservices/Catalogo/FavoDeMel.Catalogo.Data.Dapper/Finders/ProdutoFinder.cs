using FavoDeMel.Catalogo.Data.Dapper.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Catalogo.Data.Dapper.ExtensionsMethods;
using Dapper;
using System.Data;
using System;
using FavoDeMel.Catalogo.Application.Interfaces;
using FavoDeMel.Catalogo.Domain.Models;
using FavoDeMel.Catalogo.Application.ViewModels;

namespace FavoDeMel.Catalogo.Data.Dapper.Finders
{
    public class ProdutoFinder : FinderSql, IProdutoFinder
    {
        public ProdutoFinder(IBackendConnectionFactory connection) : base(connection)
        {
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            try
            {
                using var cnn = ConnectionFactory.CreateManaged();
                var data = await cnn.QueryAsync<ProdutoViewModel>($"select * from Produto");
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter os dados", ex);
            }
        }


    }
}