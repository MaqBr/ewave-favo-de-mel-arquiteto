using FavoDeMel.Presentation.MVC.CatalogoViewModels.Venda.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.Services
{
    public interface IComandaAppService
    {
        Task<ComandaViewModel> ObterComandaCliente(Guid clienteId);
        Task FinalizarComanda(FinalizarComandaDTO comanda);
        Task CancelarComanda(CancelarComandaDTO comanda);
        Task AdicionarItemComanda(AdicionarItemComandaDTO itemComanda);
        Task AtualizarItemComanda(AtualizarItemComandaDTO itemComanda);
        Task RemoverItemComanda(RemoverItemComandaDTO itemComanda);
    }
}
