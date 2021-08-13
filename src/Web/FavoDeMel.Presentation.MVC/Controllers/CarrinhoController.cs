using System;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Domain.Core.Messages.CommonMessages.Notifications;
using FavoDeMel.Presentation.MVC.CatalogoViewModels.Venda.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using FavoDeMel.Presentation.MVC.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FavoDeMel.Presentation.MVC.Controllers
{
    public class CarrinhoController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IPedidoAppService _pedidoAppService;
        private string returnView => User.Identity.IsAuthenticated && User.Identity.Name.Equals("cozinha")
            ? "IndexCozinha" : "IndexGarcom";

        public CarrinhoController(INotificationHandler<DomainNotification> notifications,
                                  IProdutoAppService produtoAppService, 
                                  IMediatorHandler mediatorHandler, 
                                  IPedidoAppService pedidoAppService,
                                  IHttpContextAccessor httpContextAccessor
            ) : base(notifications, mediatorHandler, httpContextAccessor)
        {
            _produtoAppService = produtoAppService;
            _pedidoAppService = pedidoAppService;
        }

        [Route("vizualizar-comanda/garcom")]
        public async Task<IActionResult> IndexGarcom()
        {
            var model = await _pedidoAppService.ObterCarrinhoCliente(ClienteId);

            if(model == null)
                model = new CarrinhoViewModel { ClienteId = ClienteId };

            return View(model);
        }

        [Route("vizualizar-comanda/cozinha")]
        public async Task<IActionResult> IndexCozinha()
        {
            var model = await _pedidoAppService.ObterCarrinhoCliente(ClienteId);
            if (model == null)
                model = new CarrinhoViewModel { ClienteId = ClienteId };

            return View(model);
        }

        [HttpPost]
        [Route("comanda")]
        public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            if (produto.QuantidadeEstoque < quantidade)
            {
                TempData["Erro"] = "Produto com estoque insuficiente";
                return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
            }

            await _pedidoAppService
                .AdicionarItemPedido(new AdicionarItemPedidoDTO { 
                    ClienteId = ClienteId, 
                    ProdutoId = produto.Id, 
                    Nome = produto.Nome, 
                    Quantidade = quantidade, 
                    ValorUnitario = produto.Valor 
                });

            if (OperacaoValida())
            {
                return RedirectToAction("Index", "Vitrine");
            }

            TempData["Erros"] = ObterMensagensErro();
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }

        [HttpPost]
        [Route("remover-item")]
        public async Task<IActionResult> RemoverItem(Guid id)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            await _pedidoAppService
                    .RemoverItemPedido(new RemoverItemPedidoDTO
                    {
                        ClienteId = ClienteId,
                        ProdutoId = id,
                    });

            if (OperacaoValida())
            {
                return RedirectToAction(returnView);
            }
            
            return View("Index", await _pedidoAppService.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost]
        [Route("atualizar-item")]
        public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            await _pedidoAppService
                    .AtualizarItemPedido(new AtualizarItemPedidoDTO
                    {
                        ClienteId = ClienteId,
                        ProdutoId = produto.Id,
                        Quantidade = quantidade,
                    });

            if (OperacaoValida())
            {
                return RedirectToAction(returnView);
            }

            return View("Index", await _pedidoAppService.ObterCarrinhoCliente(ClienteId));
        }

        [Route("resumo-da-compra/finalizar")]
        public async Task<IActionResult> ResumoDaCompra()
        {
            return View(await _pedidoAppService.ObterCarrinhoCliente(ClienteId));
        }

        [Route("resumo-da-compra/cancelar")]
        public async Task<IActionResult> ResumoDaCompraCancelar()
        {
            return View(await _pedidoAppService.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost]
        [Route("finalizar-pedido")]
        public async Task<IActionResult> FinalizarPedido(CarrinhoViewModel carrinhoViewModel)
        {
            var carrinho = await _pedidoAppService.ObterCarrinhoCliente(ClienteId);

            await _pedidoAppService
                    .FinalizarPedido(new FinalizarPedidoDTO
                    {

                        PedidoId = carrinho.PedidoId,
                        ClienteId = ClienteId

                    });

            if (OperacaoValida())
            {
                 return RedirectToAction("Index", "Vitrine");
             }

             return View("ResumoDaCompra", await _pedidoAppService.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost]
        [Route("cancelar-pedido")]
        public async Task<IActionResult> CancelarPedido(CarrinhoViewModel carrinhoViewModel)
        {
            var carrinho = await _pedidoAppService.ObterCarrinhoCliente(ClienteId);

            await _pedidoAppService
                    .CancelarPedido(new CancelarPedidoDTO
                    {
                        PedidoId = carrinho.PedidoId,
                        ClienteId = ClienteId
                    });

            if (OperacaoValida())
            {
                return RedirectToAction("Index", "Vitrine");
            }

            return View("ResumoDaCompraCancelar", await _pedidoAppService.ObterCarrinhoCliente(ClienteId));
        }
    }
}