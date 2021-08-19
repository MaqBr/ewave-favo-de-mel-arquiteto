using FavoDeMel.Presentation.MVC.ViewModels;
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