using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FavoDeMel.Catalogo.Application.Services;
using FavoDeMel.Catalogo.Application.ViewModels;
using FavoDeMel.Catalogo.Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FavoDeMel.Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoAppService _produtoService;
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(IProdutoAppService produtoService, ILogger<ProdutoController> logger)
        {
            _logger = logger;
            _produtoService = produtoService;
        }

        [HttpGet]
        [Route("vitrine")]
        [ProducesResponseType(typeof(DadosPaginadoDTO<ProdutoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterTodos([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            try
            {
                var produtos = await _produtoService.ObterTodos(pageSize, pageIndex);
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao obter os dados");
                throw new Exception($"Houve um erro ao obter os dados: {ex.Message}");
            }

        }


        [HttpGet]
        [Route("categorias")]
        public async Task<IEnumerable<CategoriaViewModel>> ObterCategorias()
        {
            try
            {
                return await _produtoService.ObterCategorias();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao obter os dados");
                throw new Exception($"Houve um erro ao obter os dados: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("categoria/{id}")]
        public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int id)
        {
            try
            {
                return await _produtoService.ObterPorCategoria(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao obter os dados");
                throw new Exception($"Houve um erro ao obter os dados: {ex.Message}");
            }

        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<ProdutoViewModel> ProdutoDetalhe(Guid id)
        {
            try
            {
                var produto = await _produtoService.ObterPorId(id);
                return produto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao obter os dados");
                throw new Exception($"Houve um erro ao obter os dados: {ex.Message}");
            }

        }

    }
}
