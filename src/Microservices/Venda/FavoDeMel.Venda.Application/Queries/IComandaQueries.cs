using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Venda.Application.Queries.ViewModels;
using FavoDeMel.Venda.Domain.Models;

namespace FavoDeMel.Venda.Application.Queries
{
    public interface IComandaQueries
    {
        Task<ComandaViewModel> ObterComandaMesa(Guid mesaId);
        Task<IEnumerable<ComandaViewModel>> ObterComandasMesa(Guid mesaId);
        Task<IEnumerable<ComandaViewModel>> ObterComandaStatus(ComandaStatus status);
    }
}