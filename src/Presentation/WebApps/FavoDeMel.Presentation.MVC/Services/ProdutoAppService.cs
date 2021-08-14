using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Extensions;
using FavoDeMel.Domain.Core.Model.Configuration;
using FavoDeMel.Presentation.MVC.CatalogoViewModels.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebMVC.Infrastructure;

namespace FavoDeMel.Presentation.MVC.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProdutoAppService> _logger;
        private IConfiguration _configuration;
        private readonly string _remoteServiceBaseUrl;

        public ProdutoAppService(HttpClient httpClient, 
            ILogger<ProdutoAppService> logger,
            IConfiguration configuration)
        {

            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;
            _appSettings = configuration.GetAppSettings();
            _remoteServiceBaseUrl = $"{_appSettings.Microservices.CatalogoBaseUrl}";
        }


        public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo)
        {

            
            var uri = API.Produto.ObterPorCategoria(_remoteServiceBaseUrl, codigo);

            var responseString = await _httpClient.GetStringAsync(uri);

            var produtos = JsonConvert.DeserializeObject<IEnumerable<ProdutoViewModel>>(responseString);

            return produtos;

        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {

            var uri = API.Produto.ObterPorId(_remoteServiceBaseUrl, id);

            var responseString = await _httpClient.GetStringAsync(uri);

            var produto = JsonConvert.DeserializeObject<ProdutoViewModel>(responseString);

            return produto;

        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            var uri = API.Produto.ObterTodos(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            var produtos = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(responseString);

            return produtos;
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategorias()
        {
            var uri = API.Produto.ObterCategorias(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            var categorias = JsonConvert.DeserializeObject<List<CategoriaViewModel>>(responseString);

            return categorias;
        }

        public async Task AdicionarProduto(ProdutoViewModel produtoViewModel)
        {
            var uri = API.Produto.Adicionar(_remoteServiceBaseUrl);
            var produtoContent = new StringContent(JsonConvert.SerializeObject(produtoViewModel),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, produtoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao adicionar o produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task AtualizarProduto(ProdutoViewModel produtoViewModel)
        {
            var uri = API.Produto.Atualizar(_remoteServiceBaseUrl);
            var produtoContent = new StringContent(JsonConvert.SerializeObject(produtoViewModel),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, produtoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao atualizar o produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task DebitarEstoque(AtualizarEstoqueDTO produto)
        {
            var uri = API.Produto.DebitarEstoque(_remoteServiceBaseUrl);
            var produtoContent = new StringContent(JsonConvert.SerializeObject(produto),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, produtoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao debitar o estoque.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task ReporEstoque(AtualizarEstoqueDTO produto)
        {
            var uri = API.Produto.ReporEstoque(_remoteServiceBaseUrl);
            var produtoContent = new StringContent(JsonConvert.SerializeObject(produto),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, produtoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao debitar o estoque.");
            }

            response.EnsureSuccessStatusCode();
        }

    }
}