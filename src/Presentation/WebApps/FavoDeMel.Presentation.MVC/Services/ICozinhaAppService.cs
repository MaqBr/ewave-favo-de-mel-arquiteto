using FavoDeMel.Presentation.MVC.Models.DTO;
using FavoDeMel.Presentation.MVC.Venda.ViewModels;
using FavoDeMel.Presentation.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.Services
{
    public interface ICozinhaAppService
    {
        Task<IEnumerable<ComandaViewModel>> ObterComandasPorStatus(ComandaStatus status);
        Task<ComandaViewModel> ObterComandaMesa(Guid mesaId);
    }
}
