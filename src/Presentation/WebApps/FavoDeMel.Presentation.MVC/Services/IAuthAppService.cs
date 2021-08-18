using FavoDeMel.Presentation.MVC.CatalogoViewModels.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using FavoDeMel.Presentation.MVC.ViewModels;
using FavoDeMel.Presentation.MVC.ViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.Services
{
    public interface IAuthAppService
    {
        Task<UsuarioAutenticadoViewModel> Autenticar(UsuarioViewModel usuario);
    }
}