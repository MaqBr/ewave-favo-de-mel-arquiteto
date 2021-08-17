using System;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Domain.Core.Messages.CommonMessages.Notifications;
using FavoDeMel.Presentation.MVC.CatalogoViewModels.Venda.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using FavoDeMel.Presentation.MVC.Services;
using FavoDeMel.Presentation.MVC.ViewModels.VendaViewModels.Enuns;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FavoDeMel.Presentation.MVC.Controllers
{
    public class ComandaController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IComandaAppService _pedidoAppService;
        private string returnView => User.Identity.IsAuthenticated && User.Identity.Name.Equals("cozinha")
            ? "IndexCozinha" : "IndexGarcom";

        public ComandaController(INotificationHandler<DomainNotification> notifications,
                                  IProdutoAppService produtoAppService, 
                                  IMediatorHandler mediatorHandler, 
                                  IComandaAppService pedidoAppService,
                                  IHttpContextAccessor httpContextAccessor
            ) : base(notifications, mediatorHandler, httpContextAccessor)
        {
            _produtoAppService = produtoAppService;
            _pedidoAppService = pedidoAppService;
        }

        [Route("vizualizar-comanda/garcom")]
        public async Task<IActionResult> IndexGarcom()
        {
            var model = await _pedidoAppService.ObterComandaCliente(ClienteId);

            if(model == null)
                model = new ComandaViewModel { ClienteId = ClienteId };

            return View(model);
        }

        [Route("vizualizar-comanda/cozinha")]
        public async Task<IActionResult> IndexCozinha()
        {
            var model = await _pedidoAppService.ObterComandaCliente(ClienteId);
            if (model == null)
                model = new ComandaViewModel { ClienteId = ClienteId };

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
                .AdicionarItemComanda(new AdicionarItemComandaDTO { 
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
                    .RemoverItemComanda(new RemoverItemComandaDTO
                    {
                        ClienteId = ClienteId,
                        ProdutoId = id,
                    });

            if (OperacaoValida())
            {
                return RedirectToAction(returnView);
            }
            
            return View("Index", await _pedidoAppService.ObterComandaCliente(ClienteId));
        }

        [HttpPost]
        [Route("atualizar-item")]
        public async Task<IActionResult> AtualizarItem(Guid id, int quantidade, ItemStatus itemStatus)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            await _pedidoAppService
                    .AtualizarItemComanda(new AtualizarItemComandaDTO
                    {
                        ClienteId = ClienteId,
                        ProdutoId = produto.Id,
                        Quantidade = quantidade,
                        ItemStatus = itemStatus
                    });

            if (OperacaoValida())
            {
                return RedirectToAction(returnView);
            }

            return View("Index", await _pedidoAppService.ObterComandaCliente(ClienteId));
        }

        [Route("resumo-da-compra/finalizar")]
        public async Task<IActionResult> ResumoDaCompra()
        {
            return View(await _pedidoAppService.ObterComandaCliente(ClienteId));
        }

        [Route("resumo-da-compra/cancelar")]
        public async Task<IActionResult> ResumoDaCompraCancelar()
        {
            return View(await _pedidoAppService.ObterComandaCliente(ClienteId));
        }

        [HttpPost]
        [Route("finalizar-comanda")]
        public async Task<IActionResult> FinalizarComanda(ComandaViewModel comandaViewModel)
        {
            var carrinho = await _pedidoAppService.ObterComandaCliente(ClienteId);

            await _pedidoAppService
                    .FinalizarComanda(new FinalizarComandaDTO
                    {

                        ComandaId = carrinho.ComandaId,
                        ClienteId = ClienteId

                    });

            if (OperacaoValida())
            {
                 return RedirectToAction("Index", "Vitrine");
             }

             return View("ResumoDaCompra", await _pedidoAppService.ObterComandaCliente(ClienteId));
        }

        [HttpPost]
        [Route("cancelar-comanda")]
        public async Task<IActionResult> CancelarComanda(ComandaViewModel comandaViewModel)
        {
            var carrinho = await _pedidoAppService.ObterComandaCliente(ClienteId);

            await _pedidoAppService
                    .CancelarComanda(new CancelarComandaDTO
                    {
                        ComandaId = carrinho.ComandaId,
                        ClienteId = ClienteId
                    });

            if (OperacaoValida())
            {
                return RedirectToAction("Index", "Vitrine");
            }

            return View("ResumoDaCompraCancelar", await _pedidoAppService.ObterComandaCliente(ClienteId));
        }
    }
}