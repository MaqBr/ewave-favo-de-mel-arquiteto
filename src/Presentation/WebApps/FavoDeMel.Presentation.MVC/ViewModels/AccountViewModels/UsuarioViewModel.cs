﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FavoDeMel.Presentation.MVC.ViewModels.AccountViewModels
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "O e-mail deve ser inserido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha deve ser inserida.")]
        public string Password { get; set; }

        public bool Success { get; set; }

        public LoginResponseViewModel Data { get; set; }

    }

    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }

    public class UsuarioAutenticadoViewModel
    {
        public bool Success { get; set; }
        public LoginResponseViewModel Data { get; set; }
    }

    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }

    public class ClaimViewModel
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
