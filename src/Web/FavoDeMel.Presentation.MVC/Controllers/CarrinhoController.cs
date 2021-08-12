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

        [Route("meu-carrinho")]
        public async Task<IActionResult> Index()
        {
            return View(await _pedidoAppService.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost]
        [Route("meu-carrinho")]
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
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }

            return View("Index", await _pedidoAppService.ObterCarrinhoCliente(ClienteId));
        }

        [Route("resumo-da-compra")]
        public async Task<IActionResult> ResumoDaCompra()
        {
            return View(await _pedidoAppService.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost]
        [Route("iniciar-pedido")]
        public async Task<IActionResult> IniciarPedido(CarrinhoViewModel carrinhoViewModel)
        {
            var carrinho = await _pedidoAppService.ObterCarrinhoCliente(ClienteId);

            await _pedidoAppService
                    .IniciarPedido(new IniciarPedidoDTO
                    {

                        PedidoId = carrinho.PedidoId,
                        ClienteId = ClienteId,
                        Total = carrinho.ValorTotal,
                        NomeCartao = carrinhoViewModel.Pagamento.NomeCartao,
                        NumeroCartao = carrinhoViewModel.Pagamento.NumeroCartao,
                        ExpiracaoCartao = carrinhoViewModel.Pagamento.ExpiracaoCartao,
                        CvvCartao = carrinhoViewModel.Pagamento.CvvCartao
                    });

            if (OperacaoValida())
             {
                 return RedirectToAction("Index", "Pedido");
             }

             return View("ResumoDaCompra", await _pedidoAppService.ObterCarrinhoCliente(ClienteId));
        }
    }
}