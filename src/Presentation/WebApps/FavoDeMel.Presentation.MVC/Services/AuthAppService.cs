using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Extensions;
using FavoDeMel.Domain.Core.Model.Configuration;
using FavoDeMel.Presentation.MVC.CatalogoViewModels.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using FavoDeMel.Presentation.MVC.ViewModels;
using FavoDeMel.Presentation.MVC.ViewModels.AccountViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebMVC.Infrastructure;

namespace FavoDeMel.Presentation.MVC.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthAppService> _logger;
        private IConfiguration _configuration;
        private readonly string _remoteServiceBaseUrl;

        public AuthAppService(HttpClient httpClient, 
            ILogger<AuthAppService> logger,
            IConfiguration configuration)
        {

            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;
            _appSettings = configuration.GetAppSettings();
            _remoteServiceBaseUrl = $"{_appSettings.Microservices.IdentityBaseUrl}";
        }

        public async Task<UsuarioAutenticadoViewModel> Autenticar(UsuarioViewModel usuario)
        {
            var uri = API.Usuario.Autenticar(_remoteServiceBaseUrl);
            var usuarioAutenticado = new StringContent(JsonConvert.SerializeObject(usuario),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, usuarioAutenticado);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error autenticar o usuário.");
            }

            var jsonResposta = response.Content
                .ReadAsStringAsync().GetAwaiter().GetResult();

            var usuarioResponse = JsonConvert.DeserializeObject<UsuarioAutenticadoViewModel>(jsonResposta) ;

            return usuarioResponse;
        }
    }
}