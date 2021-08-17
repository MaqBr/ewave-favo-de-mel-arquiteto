using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Extensions;
using FavoDeMel.Domain.Core.Model.Configuration;
using FavoDeMel.Presentation.MVC.CatalogoViewModels.Venda.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebMVC.Infrastructure;

namespace FavoDeMel.Presentation.MVC.Services
{
    public class ComandaAppService : IComandaAppService
    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProdutoAppService> _logger;
        private readonly string _remoteServiceBaseUrl;
        private IConfiguration _configuration;
        public ComandaAppService(HttpClient httpClient, 
            ILogger<ProdutoAppService> logger,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;
            _appSettings = configuration.GetAppSettings();
            _remoteServiceBaseUrl = $"{_appSettings.Microservices.VendaBaseUrl}";
            
        }

        public async Task<ComandaViewModel> ObterComandaCliente(Guid clienteId)
        {
            
            var uri = API.Comanda.ObterComandaCliente(_remoteServiceBaseUrl, clienteId);

            var responseString = await _httpClient.GetStringAsync(uri);

            var carrinho = JsonConvert.DeserializeObject<ComandaViewModel>(responseString);

            return carrinho;
        }

        public async Task AdicionarItemComanda(AdicionarItemComandaDTO itemComanda)
        {
            var uri = API.Comanda.AdicionarItemComanda(_remoteServiceBaseUrl);

            var comandaContent = new StringContent(JsonConvert.SerializeObject(itemComanda),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, comandaContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao adicionar o item do comanda produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task AtualizarItemComanda(AtualizarItemComandaDTO itemComanda)
        {
            var uri = API.Comanda.AtualizarItemComanda(_remoteServiceBaseUrl);

            var comandaContent = new StringContent(JsonConvert.SerializeObject(itemComanda),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, comandaContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao atualizar o item do comanda produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task RemoverItemComanda(RemoverItemComandaDTO itemComanda)
        {
            var uri = API.Comanda.RemoverItemComanda(_remoteServiceBaseUrl);

            var comandaContent = new StringContent(JsonConvert.SerializeObject(itemComanda),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, comandaContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao remover o item da comanda do produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task FinalizarComanda(FinalizarComandaDTO comanda)
        {
            var uri = API.Comanda.FinalizarComanda(_remoteServiceBaseUrl);

            var comandaContent = new StringContent(JsonConvert.SerializeObject(comanda),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, comandaContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao finalizar a comanda do produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task CancelarComanda(CancelarComandaDTO comanda)
        {
            var uri = API.Comanda.CancelarComanda(_remoteServiceBaseUrl);

            var comandaContent = new StringContent(JsonConvert.SerializeObject(comanda),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, comandaContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao cancelar o comanda do produto.");
            }

            response.EnsureSuccessStatusCode();
        }
    }
}