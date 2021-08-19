using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Extensions;
using FavoDeMel.Domain.Core.Model.Configuration;
using FavoDeMel.Presentation.MVC.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebMVC.Infrastructure;

namespace FavoDeMel.Presentation.MVC.Services
{
    public class MesaAppService : IMesaAppService
    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<MesaAppService> _logger;
        private IConfiguration _configuration;
        private readonly string _remoteServiceBaseUrl;

        public MesaAppService(HttpClient httpClient, 
            ILogger<MesaAppService> logger,
            IConfiguration configuration)
        {

            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;
            _appSettings = configuration.GetAppSettings();
            _remoteServiceBaseUrl = $"{_appSettings.Microservices.VendaBaseUrl}";
        }


        public async Task<IEnumerable<MesaViewModel>> ObterTodos()
        {
            var uri = API.Mesa.ObterTodos(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            var mesas = JsonConvert.DeserializeObject<IEnumerable<MesaViewModel>>(responseString);

            return mesas.OrderBy(x=> x.Numero);
        }

        public async Task<MesaViewModel> ObterPorId(Guid id)
        {

            var mesas = await ObterTodos();

            var returnMesa = mesas.FirstOrDefault(x => x.MesaId == id);

            return returnMesa;

            /*
            TODO: Identificar o motivo pelo qual sempre retorna 404

            var uri = API.Mesa.ObterPorId(_remoteServiceBaseUrl, id);

            var responseString = await _httpClient.GetStringAsync(uri);

            var mesa = JsonConvert.DeserializeObject<MesaViewModel>(responseString);

            return mesa;
            */
        }
    }
}