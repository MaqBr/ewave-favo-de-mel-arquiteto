﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Catalogo.Application.Interfaces;
using FavoDeMel.Catalogo.Application.Models;
using FavoDeMel.Catalogo.Application.Services;
using FavoDeMel.Catalogo.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FavoDeMel.Catalogo.Api.Controllers
{
    [Route("api/[controller]"), Authorize]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly ICatalogoService _accountService;
        private readonly IProdutoAppService _produtoService;
        private readonly ILogger<CatalogoController> _logger;

        public CatalogoController(ICatalogoService accountService, IProdutoAppService produtoService, ILogger<CatalogoController> logger)
        {
            _accountService = accountService;
            _logger = logger;
            _produtoService = produtoService;
        }

        [HttpGet]
        [Route("produto-detalhe/categoria/{id}")]
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


        // GET api/Catalogo
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                _logger.LogTrace("Executando o método get para obter contas");
                var accounts = await _accountService.GetAccounts();
                return new JsonResult(accounts);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao obter os dados");
                throw new Exception($"Houve um erro ao obter os dados: {ex.Message}");
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody] AccountVenda accountVenda)
        {
            try
            {
                _logger.LogTrace("Executando o método post para Vendaência entre contas");
                _accountService.Venda(accountVenda);
                return Ok(accountVenda);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao enviar os dados {accountVenda}", accountVenda);
                throw new Exception($"Houve um erro ao enviar os dados: {ex.Message}");
            }

        }
    }
}
