using System;
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
    [Route("api/[controller]")]
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

        [HttpGet]
        [Route("meu-carrinho")]
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

        [HttpPost]
        [Route("meu-carrinho/adicionar")]
        public async Task<IActionResult> AdicionarItem([FromBody]AdicionarItemPedidoDTO itemPedido)
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
    }
}
