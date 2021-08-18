using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FavoDeMel.Presentation.MVC.ViewModels.AccountViewModels
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "O e-mail deve ser inserido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha deve ser inserida.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar?")]
        public bool Lembrar { get; set; }
        public string ReturnUrl { get; set; }
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
        public bool Lembrar { get; set; }
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
