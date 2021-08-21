using System;
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
        [Route("adicionar")]
        public async Task<IActionResult> AdicionarComanda([FromBody] AdicionarComandaDTO comanda)
        {
            if (comanda == null) return BadRequest();

            try
            {
                var command = new AdicionarComandaCommand(Guid.NewGuid(), comanda.MesaId, comanda.Codigo);

                await _mediatorHandler.EnviarComando(command);

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao adicionar a comanda {itemComanda}", comanda);
                throw new Exception($"Houve um erro ao adicionar a comanda: {ex.Message}");
            }

        }


        [HttpPost]
        [Route("item/adicionar")]
        public async Task<IActionResult> AdicionarItem([FromBody] AdicionarItemComandaDTO itemComanda)
        {
            if (itemComanda == null) return BadRequest();

            try
            {
                var command = new AdicionarItemComandaCommand(itemComanda.MesaId,
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
                var command = new AtualizarItemComandaCommand(itemComanda.MesaId,
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
                var command = new RemoverItemComandaCommand(itemComanda.MesaId,
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
        [Route("iniciar")]
        public async Task<IActionResult> IniciarComanda(IniciarComandaDTO comanda)
        {
            try
            {
                var resultado = await _comandaQueries.ObterComandaMesa(comanda.MesaId);

                var command = new IniciarComandaCommand(resultado.ComandaId, resultado.MesaId.Value, comanda.Total);

                await _mediatorHandler.EnviarComando(command);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao iniciar a comanda {comanda}", comanda);
                throw new Exception($"Houve um erro ao iniciar a comanda: {ex.Message}");
            }

        }

        [HttpPost]
        [Route("entregar")]
        public async Task<IActionResult> EntregarComanda(EntregarComandaDTO comanda)
        {
            try
            {
                var resultado = await _comandaQueries.ObterComandaMesa(comanda.MesaId);

                var command = new EntregarComandaCommand(resultado.ComandaId, resultado.MesaId.Value, comanda.Total);

                await _mediatorHandler.EnviarComando(command);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao entregar a comanda {comanda}", comanda);
                throw new Exception($"Houve um erro ao entregar a comanda: {ex.Message}");
            }

        }

        [HttpPost]
        [Route("finalizar")]
        public async Task<IActionResult> FinalizarComanda(FinalizarComandaDTO comanda)
        {
            try
            {
                var resultado = await _comandaQueries.ObterComandaMesa(comanda.MesaId);

                var command = new FinalizarComandaCommand(resultado.ComandaId, resultado.MesaId.Value);

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
                var resultado = await _comandaQueries.ObterComandaMesa(comanda.MesaId);

                var command = new CancelarComandaCommand(resultado.ComandaId, resultado.MesaId.Value);

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
        [Route("mesa/{mesaId}")]
        public async Task<ComandaViewModel> ObterComandaMesa(Guid mesaId)
        {
            try
            {
                var result = await _comandaQueries.ObterComandaMesa(mesaId);
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
