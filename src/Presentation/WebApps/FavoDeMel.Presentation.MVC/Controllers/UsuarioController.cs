using FavoDeMel.Presentation.MVC.Services;
using FavoDeMel.Presentation.MVC.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IAuthAppService _authAppService;
        public UsuarioController(ILogger<UsuarioController> logger, IAuthAppService authAppService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authAppService = authAppService;
        }

        public IActionResult Entrar()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Vitrine");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(UsuarioViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuarioAutenticado = await _authAppService.Autenticar(usuario);

                    if(usuarioAutenticado.Success)
                    {
                        Login(usuarioAutenticado);
                        return RedirectToAction("Index", "Vitrine");
                    }
                    else
                    {
                        ViewBag.Erro = "E-mail e / ou senha incorretos!";
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Erro = "Ocorreu algum erro na autententicação. Por favor, tente novamente...";
            }
            return View();
        }

        private async void Login(UsuarioAutenticadoViewModel usuarioAutenticado)
        {
            //TODO: obter token e claims
            var claimsUser = usuarioAutenticado.Data.UserToken.Claims;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuarioAutenticado.Data.UserToken.Email),
            };

            var identidadeDeUsuario = new ClaimsIdentity(claims, "Email");
            var claimPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

            var propriedadesDeAutenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(2),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);
        }


        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }


    }
}
