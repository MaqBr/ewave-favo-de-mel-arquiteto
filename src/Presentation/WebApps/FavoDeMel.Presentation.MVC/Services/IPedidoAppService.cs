using FavoDeMel.Presentation.MVC.CatalogoViewModels.Venda.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.Services
{
    public interface IPedidoAppService
    {
        Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId);
        Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clienteId);
        Task FinalizarPedido(FinalizarPedidoDTO pedido);
        Task CancelarPedido(CancelarPedidoDTO pedido);
        Task AdicionarItemPedido(AdicionarItemPedidoDTO itemPedido);
        Task AtualizarItemPedido(AtualizarItemPedidoDTO itemPedido);
        Task RemoverItemPedido(RemoverItemPedidoDTO itemPedido);
    }
}
