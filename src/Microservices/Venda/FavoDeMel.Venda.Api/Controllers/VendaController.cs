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

        private readonly IPedidoQueries _pedidoQueries;
        private readonly ILogger<VendaController> _logger;
        private readonly IMediatorHandler _mediatorHandler;

        public VendaController(IPedidoQueries pedidoQueries, 
            ILogger<VendaController> logger,
            IMediatorHandler mediatorHandler)
        {
            _logger = logger;
            _pedidoQueries = pedidoQueries;
            _mediatorHandler = mediatorHandler;

        }
        [HttpPost]
        [Route("item/adicionar")]
        public async Task<IActionResult> AdicionarItem([FromBody] AdicionarItemPedidoDTO itemPedido)
        {
            if (itemPedido == null) return BadRequest();

            try
            {
                var command = new AdicionarItemPedidoCommand(itemPedido.ClienteId,
                    itemPedido.ProdutoId, itemPedido.Nome, itemPedido.Quantidade, itemPedido.ValorUnitario);

                await _mediatorHandler.EnviarComando(command);

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao adicionar o item no carrinho {itemPedido}", itemPedido);
                throw new Exception($"Houve um erro ao adicionar o item no carrinho: {ex.Message}");
            }

        }

        [HttpPut]
        [Route("item/atualizar")]
        public async Task<IActionResult> AtualizarItem([FromBody] AtualizarItemPedidoDTO itemPedido)
        {
            if (itemPedido == null) return BadRequest();

            try
            {
                var command = new AtualizarItemPedidoCommand(itemPedido.ClienteId,
                    itemPedido.ProdutoId, itemPedido.Quantidade, itemPedido.ItemStatus);

                await _mediatorHandler.EnviarComando(command);

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao atualizar o item no carrinho {itemPedido}", itemPedido);
                throw new Exception($"Houve um erro ao atualizar o item no carrinho: {ex.Message}");
            }

        }

        [HttpPost]
        [Route("item/remover")]
        public async Task<IActionResult> RemoverItem([FromBody] RemoverItemPedidoDTO itemPedido)
        {
            if (itemPedido == null) return BadRequest();

            try
            {
                var command = new RemoverItemPedidoCommand(itemPedido.ClienteId,
                    itemPedido.ProdutoId);

                await _mediatorHandler.EnviarComando(command);

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao remover o item do carrinho {itemPedido}", itemPedido);
                throw new Exception($"Houve um erro ao remover o item do carrinho: {ex.Message}");
            }


        }

        [HttpPost]
        [Route("finalizar")]
        public async Task<IActionResult> FinalizarPedido(FinalizarPedidoDTO pedido)
        {
            try
            {
                var carrinho = await _pedidoQueries.ObterCarrinhoCliente(pedido.ClienteId);

                var command = new FinalizarPedidoCommand(carrinho.PedidoId, pedido.ClienteId);

                await _mediatorHandler.EnviarComando(command);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao finalizar o pedido {pedido}", pedido);
                throw new Exception($"Houve um erro ao finalizar o pedido: {ex.Message}");
            }

        }

        [HttpPost]
        [Route("cancelar")]
        public async Task<IActionResult> CancelarPedido(CancelarPedidoDTO pedido)
        {
            try
            {
                var carrinho = await _pedidoQueries.ObterCarrinhoCliente(pedido.ClienteId);

                var command = new CancelarPedidoCommand(carrinho.PedidoId, pedido.ClienteId);

                await _mediatorHandler.EnviarComando(command);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao cancelar o pedido {pedido}", pedido);
                throw new Exception($"Houve um erro ao cancelar o pedido: {ex.Message}");
            }

        }

        [HttpGet]
        [Route("cliente/{clienteId}")]
        public async Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId)
        {
            try
            {
                var result = await _pedidoQueries.ObterCarrinhoCliente(clienteId);
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
        public async Task<IEnumerable<PedidoViewModel>> ObterPedidoStatus(PedidoStatus status)
        {
            try
            {
                var result = await _pedidoQueries.ObterPedidosStatus(status);
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
