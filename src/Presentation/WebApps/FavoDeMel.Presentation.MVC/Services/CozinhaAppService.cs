using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Extensions;
using FavoDeMel.Domain.Core.Model.Configuration;
using FavoDeMel.Presentation.MVC.Venda.ViewModels;
using FavoDeMel.Presentation.MVC.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebMVC.Infrastructure;

namespace FavoDeMel.Presentation.MVC.Services
{
    public class CozinhaAppService : ICozinhaAppService
    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CozinhaAppService> _logger;
        private readonly string _remoteServiceBaseUrl;
        private IConfiguration _configuration;
        public CozinhaAppService(HttpClient httpClient, 
            ILogger<CozinhaAppService> logger,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;
            _appSettings = configuration.GetAppSettings();
            _remoteServiceBaseUrl = $"{_appSettings.Microservices.VendaBaseUrl}";
            
        }

        public async Task<IEnumerable<ComandaViewModel>> ObterComandasPorStatus(ComandaStatus status)
        {
            var uri = API.Cozinha.ObterComandaStatus(_remoteServiceBaseUrl, status);

            var responseString = await _httpClient.GetStringAsync(uri);

            var comandas = JsonConvert.DeserializeObject<IEnumerable<ComandaViewModel>>(responseString);

            return comandas;
        }

        public async Task<ComandaViewModel> ObterComandaMesa(Guid mesaId)
        {

            var uri = API.Cozinha.ObterComandaMesa(_remoteServiceBaseUrl, mesaId);

            var responseString = await _httpClient.GetStringAsync(uri);

            var comanda = JsonConvert.DeserializeObject<ComandaViewModel>(responseString);

            return comanda;
        }

    }
}