using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Venda.Application.Queries.ViewModels;

namespace FavoDeMel.Venda.Application.Queries
{
    public interface IPedidoQueries
    {
        Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId);
        Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clienteId);
    }
}