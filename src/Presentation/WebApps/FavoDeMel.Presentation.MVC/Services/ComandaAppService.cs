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

        public async Task AdicionarItemComanda(AdicionarItemComandaDTO itemPedido)
        {
            var uri = API.Comanda.AdicionarItemComanda(_remoteServiceBaseUrl);

            var pedidoContent = new StringContent(JsonConvert.SerializeObject(itemPedido),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, pedidoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao adicionar o item do pedido produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task AtualizarItemComanda(AtualizarItemComandaDTO itemPedido)
        {
            var uri = API.Comanda.AtualizarItemComanda(_remoteServiceBaseUrl);

            var pedidoContent = new StringContent(JsonConvert.SerializeObject(itemPedido),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, pedidoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao atualizar o item do pedido produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task RemoverItemComanda(RemoverItemComandaDTO itemPedido)
        {
            var uri = API.Comanda.RemoverItemComanda(_remoteServiceBaseUrl);

            var pedidoContent = new StringContent(JsonConvert.SerializeObject(itemPedido),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, pedidoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao remover o item do pedido produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task FinalizarComanda(FinalizarComandaDTO pedido)
        {
            var uri = API.Comanda.FinalizarComanda(_remoteServiceBaseUrl);

            var pedidoContent = new StringContent(JsonConvert.SerializeObject(pedido),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, pedidoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao finalizar o pedido produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task CancelarComanda(CancelarComandaDTO pedido)
        {
            var uri = API.Comanda.CancelarComanda(_remoteServiceBaseUrl);

            var pedidoContent = new StringContent(JsonConvert.SerializeObject(pedido),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, pedidoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao cancelar o pedido produto.");
            }

            response.EnsureSuccessStatusCode();
        }
    }
}