using FavoDeMel.Presentation.MVC.CatalogoViewModels.Venda.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.Services
{
    public interface IComandaAppService
    {
        Task<ComandaViewModel> ObterComandaMesa(Guid mesaId);
        Task AdicionarComanda(AdicionarComandaDTO comanda);
        Task IniciarComanda(IniciarComandaDTO comanda);
        Task FinalizarComanda(FinalizarComandaDTO comanda);
        Task CancelarComanda(CancelarComandaDTO comanda);
        Task AdicionarItemComanda(AdicionarItemComandaDTO itemComanda);
        Task AtualizarItemComanda(AtualizarItemComandaDTO itemComanda);
        Task RemoverItemComanda(RemoverItemComandaDTO itemComanda);
    }
}
