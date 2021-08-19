using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Venda.Application.Queries.ViewModels;
using FavoDeMel.Venda.Domain.Models;

namespace FavoDeMel.Venda.Application.Queries
{
    public interface IMesaQueries
    {
        Task<IEnumerable<MesaViewModel>> ObterTodos();
        Task<MesaViewModel> ObterPorId(Guid mesaId);
    }
}