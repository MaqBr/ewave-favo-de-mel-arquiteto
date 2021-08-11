﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Catalogo.Application.Services;
using FavoDeMel.Catalogo.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FavoDeMel.Catalogo.Api.Controllers
{
    //[Authorize]
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
        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            try
            {
                return await _produtoService.ObterTodos();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao obter os dados");
                throw new Exception($"Houve um erro ao obter os dados: {ex.Message}");
            }

        }


        [HttpGet]
        [Route("categoria")]
        public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo)
        {
            try
            {
                return await _produtoService.ObterPorCategoria(codigo);
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
