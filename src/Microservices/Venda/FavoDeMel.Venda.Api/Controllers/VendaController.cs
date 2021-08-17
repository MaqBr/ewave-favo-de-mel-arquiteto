﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Venda.Application;
using FavoDeMel.Venda.Application.Queries;
using FavoDeMel.Venda.Application.Queries.ViewModels;
using FavoDeMel.Venda.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FavoDeMel.Venda.Api.Controllers
{
    [Route("api/comanda")]
    [ApiController]
    public class VendaController : ControllerBase
    {

        private readonly IComandaQueries _comandaQueries;
        private readonly ILogger<VendaController> _logger;
        private readonly IMediatorHandler _mediatorHandler;

        public VendaController(IComandaQueries comandaQueries, 
            ILogger<VendaController> logger,
            IMediatorHandler mediatorHandler)
        {
            _logger = logger;
            _comandaQueries = comandaQueries;
            _mediatorHandler = mediatorHandler;

        }
        [HttpPost]
        [Route("item/adicionar")]
        public async Task<IActionResult> AdicionarItem([FromBody] AdicionarItemComandaDTO itemComanda)
        {
            if (itemComanda == null) return BadRequest();

            try
            {
                var command = new AdicionarItemComandaCommand(itemComanda.ClienteId,
                    itemComanda.ProdutoId, itemComanda.Nome, itemComanda.Quantidade, itemComanda.ValorUnitario);

                await _mediatorHandler.EnviarComando(command);

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao adicionar o item na comanda {itemComanda}", itemComanda);
                throw new Exception($"Houve um erro ao adicionar o item na comanda: {ex.Message}");
            }

        }

        [HttpPut]
        [Route("item/atualizar")]
        public async Task<IActionResult> AtualizarItem([FromBody] AtualizarItemComandaDTO itemComanda)
        {
            if (itemComanda == null) return BadRequest();

            try
            {
                var command = new AtualizarItemComandaCommand(itemComanda.ClienteId,
                    itemComanda.ProdutoId, itemComanda.Quantidade, itemComanda.ItemStatus);

                await _mediatorHandler.EnviarComando(command);

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao atualizar o item na comanda {itemComanda}", itemComanda);
                throw new Exception($"Houve um erro ao atualizar o item na comanda: {ex.Message}");
            }

        }

        [HttpPost]
        [Route("item/remover")]
        public async Task<IActionResult> RemoverItem([FromBody] RemoverItemComandaDTO itemComanda)
        {
            if (itemComanda == null) return BadRequest();

            try
            {
                var command = new RemoverItemComandaCommand(itemComanda.ClienteId,
                    itemComanda.ProdutoId);

                await _mediatorHandler.EnviarComando(command);

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao remover o item da comanda {itemComanda}", itemComanda);
                throw new Exception($"Houve um erro ao remover o item da comanda: {ex.Message}");
            }


        }

        [HttpPost]
        [Route("finalizar")]
        public async Task<IActionResult> FinalizarComanda(FinalizarComandaDTO comanda)
        {
            try
            {
                var resultado = await _comandaQueries.ObterComandaCliente(comanda.ClienteId);

                var command = new FinalizarComandaCommand(resultado.ComandaId, resultado.ClienteId);

                await _mediatorHandler.EnviarComando(command);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao finalizar a comanda {comanda}", comanda);
                throw new Exception($"Houve um erro ao finalizar a comanda: {ex.Message}");
            }

        }

        [HttpPost]
        [Route("cancelar")]
        public async Task<IActionResult> CancelarComanda(CancelarComandaDTO comanda)
        {
            try
            {
                var resultado = await _comandaQueries.ObterComandaCliente(comanda.ClienteId);

                var command = new CancelarComandaCommand(resultado.ComandaId, resultado.ClienteId);

                await _mediatorHandler.EnviarComando(command);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao cancelar a comanda {comanda}", comanda);
                throw new Exception($"Houve um erro ao cancelar a comanda: {ex.Message}");
            }

        }

        [HttpGet]
        [Route("cliente/{clienteId}")]
        public async Task<ComandaViewModel> ObterComandaCliente(Guid clienteId)
        {
            try
            {
                var result = await _comandaQueries.ObterComandaCliente(clienteId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao obter os dados");
                throw new Exception($"Houve um erro ao obter os dados: {ex.Message}");
            }

        }

        [HttpGet]
        [Route("status/{status}")]
        public async Task<IEnumerable<ComandaViewModel>> ObterComandaStatus(ComandaStatus status)
        {
            try
            {
                var result = await _comandaQueries.ObterComandaStatus(status);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao obter os dados");
                throw new Exception($"Houve um erro ao obter os dados: {ex.Message}");
            }

        }

    }
}
