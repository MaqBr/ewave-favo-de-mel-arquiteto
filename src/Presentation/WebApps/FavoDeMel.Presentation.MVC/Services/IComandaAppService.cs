using FavoDeMel.Presentation.MVC.CatalogoViewModels.Venda.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.Services
{
    public interface IComandaAppService
    {
        Task<ComandaViewModel> ObterComandaCliente(Guid clienteId);
        Task FinalizarComanda(FinalizarComandaDTO pedido);
        Task CancelarComanda(CancelarComandaDTO pedido);
        Task AdicionarItemComanda(AdicionarItemComandaDTO itemPedido);
        Task AtualizarItemComanda(AtualizarItemComandaDTO itemPedido);
        Task RemoverItemComanda(RemoverItemComandaDTO itemPedido);
    }
}
