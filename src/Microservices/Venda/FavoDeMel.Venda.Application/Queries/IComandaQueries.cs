using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Venda.Application.Queries.ViewModels;
using FavoDeMel.Venda.Domain.Models;

namespace FavoDeMel.Venda.Application.Queries
{
    public interface IComandaQueries
    {
        Task<ComandaViewModel> ObterComandaCliente(Guid clienteId);
        Task<IEnumerable<ComandaViewModel>> ObterPedidosCliente(Guid clienteId);
        Task<IEnumerable<ComandaViewModel>> ObterComandaStatus(ComandaStatus status);
    }
}