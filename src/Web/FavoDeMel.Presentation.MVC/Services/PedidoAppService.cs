using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Extensions;
using FavoDeMel.Domain.Core.Model.Configuration;
using FavoDeMel.Presentation.MVC.CatalogoViewModels.Venda.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebMVC.Infrastructure;

namespace FavoDeMel.Presentation.MVC.Services
{
    public class PedidoAppService : IPedidoAppService
    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProdutoAppService> _logger;
        private readonly string _remoteServiceBaseUrl;
        private IConfiguration _configuration;
        public PedidoAppService(HttpClient httpClient, 
            ILogger<ProdutoAppService> logger,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;
            _appSettings = configuration.GetAppSettings();
            _remoteServiceBaseUrl = $"{_appSettings.Microservices.VendaBaseUrl}/api/v1/adminprodutos";
            
        }

        public async Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId)
        {
            var uri = API.Pedido.ObterCarrinhoCliente(_remoteServiceBaseUrl, clienteId);

            var responseString = await _httpClient.GetStringAsync(uri);

            var carrinho = JsonSerializer.Deserialize<CarrinhoViewModel>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return carrinho;
        }

        public async Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clienteId)
        {
            var uri = API.Pedido.ObterPedidosCliente(_remoteServiceBaseUrl, clienteId);

            var responseString = await _httpClient.GetStringAsync(uri);

            var pedidos = JsonSerializer.Deserialize<IEnumerable<PedidoViewModel>>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return pedidos;
        }
    }
}