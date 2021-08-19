using FavoDeMel.Presentation.MVC.CatalogoViewModels.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using FavoDeMel.Presentation.MVC.ViewModels;
using FavoDeMel.Presentation.MVC.ViewModels.MesaViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.Services
{
    public interface IMesaAppService
    {
        Task<IEnumerable<MesaViewModel>> ObterTodos();
        Task<MesaViewModel> ObterPorId(Guid id);
    }
}