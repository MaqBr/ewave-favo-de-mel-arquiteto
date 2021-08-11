using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Extensions;
using FavoDeMel.Domain.Core.Model.Configuration;
using FavoDeMel.Presentation.MVC.CatalogoViewModels.ViewModels;
using Microsoft.eShopOnContainers.WebMVC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
            _remoteServiceBaseUrl = $"{_appSettings.Microservices.CatalogoBaseUrl}/api/v1/adminprodutos";
        }


        public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo)
        {

            var uri = API.Produto.ObterPorCategoria(_remoteServiceBaseUrl, codigo);

            var responseString = await _httpClient.GetStringAsync(uri);

            var produtos = JsonSerializer.Deserialize<IEnumerable<ProdutoViewModel>>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return produtos;

        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {

            var uri = API.Produto.ObterPorId(_remoteServiceBaseUrl, id);

            var responseString = await _httpClient.GetStringAsync(uri);

            var produto = JsonSerializer.Deserialize<ProdutoViewModel>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return produto;

        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            var uri = API.Produto.ObterTodos(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            var produtos = JsonSerializer.Deserialize<IEnumerable<ProdutoViewModel>>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return produtos;
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategorias()
        {
            throw new NotImplementedException();
            //return _mapper.Map<IEnumerable<CategoriaViewModel>>(await _produtoRepository.ObterCategorias());
        }

        public async Task AdicionarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = new ProdutoViewModel()
            {
                Descricao = ""
            };

            var uri = API.Produto.Adicionar(_remoteServiceBaseUrl);
            var produtoContent = new StringContent(JsonSerializer.Serialize(produto),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, produtoContent);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error ao adicionar o produto.");
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task AtualizarProduto(ProdutoViewModel produtoViewModel)
        {
            throw new NotImplementedException();
            /*var produto = _mapper.Map<Produto>(produtoViewModel);
            _produtoRepository.Atualizar(produto);

            await _produtoRepository.UnitOfWork.Commit();*/
        }

        public async Task<ProdutoViewModel> DebitarEstoque(Guid id, int quantidade)
        {
            throw new NotImplementedException();

            /*if (!_estoqueService.DebitarEstoque(id, quantidade).Result)
            {
                throw new DomainException("Falha ao debitar estoque");
            }

            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));*/
        }

        public async Task<ProdutoViewModel> ReporEstoque(Guid id, int quantidade)
        {
            throw new NotImplementedException();

            /*if (!_estoqueService.ReporEstoque(id, quantidade).Result)
            {
                throw new DomainException("Falha ao repor estoque");
            }

            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));*/
        }

    }
}