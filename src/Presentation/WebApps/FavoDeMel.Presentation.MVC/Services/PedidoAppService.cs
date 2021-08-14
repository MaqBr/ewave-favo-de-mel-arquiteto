﻿using System;
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
            _remoteServiceBaseUrl = $"{_appSettings.Microservices.VendaBaseUrl}";
            
        }

        public async Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId)
        {
            
            var uri = API.Pedido.ObterCarrinhoCliente(_remoteServiceBaseUrl, clienteId);

            var responseString = await _httpClient.GetStringAsync(uri);

            var carrinho = JsonConvert.DeserializeObject<CarrinhoViewModel>(responseString);

            return carrinho;
        }

        public async Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clienteId)
        {
            var uri = API.Pedido.ObterPedidosCliente(_remoteServiceBaseUrl, clienteId);

            var responseString = await _httpClient.GetStringAsync(uri);

            var pedidos = JsonConvert.DeserializeObject<IEnumerable<PedidoViewModel>>(responseString);

            return pedidos;
        }

        public async Task AdicionarItemPedido(AdicionarItemPedidoDTO itemPedido)
        {
            var uri = API.Pedido.AdicionarItemPedido(_remoteServiceBaseUrl);

            var pedidoContent = new StringContent(JsonConvert.SerializeObject(itemPedido),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, pedidoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao adicionar o item do pedido produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task AtualizarItemPedido(AtualizarItemPedidoDTO itemPedido)
        {
            var uri = API.Pedido.AtualizarItemPedido(_remoteServiceBaseUrl);

            var pedidoContent = new StringContent(JsonConvert.SerializeObject(itemPedido),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, pedidoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao atualizar o item do pedido produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task RemoverItemPedido(RemoverItemPedidoDTO itemPedido)
        {
            var uri = API.Pedido.RemoverItemPedido(_remoteServiceBaseUrl);

            var pedidoContent = new StringContent(JsonConvert.SerializeObject(itemPedido),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, pedidoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao remover o item do pedido produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task FinalizarPedido(FinalizarPedidoDTO pedido)
        {
            var uri = API.Pedido.FinalizarPedido(_remoteServiceBaseUrl);

            var pedidoContent = new StringContent(JsonConvert.SerializeObject(pedido),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, pedidoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao finalizar o pedido produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task CancelarPedido(CancelarPedidoDTO pedido)
        {
            var uri = API.Pedido.CancelarPedido(_remoteServiceBaseUrl);

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