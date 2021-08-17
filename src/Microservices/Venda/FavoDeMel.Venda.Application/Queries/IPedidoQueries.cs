using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Venda.Application.Queries.ViewModels;
using FavoDeMel.Venda.Domain.Models;

namespace FavoDeMel.Venda.Application.Queries
{
    public interface IPedidoQueries
    {
        Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId);
        Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clienteId);
        Task<IEnumerable<PedidoViewModel>> ObterPedidosStatus(PedidoStatus status);
    }
}