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
        private readonly IComandaAppService _comandaAppService;
        private string returnView => User.Identity.IsAuthenticated && User.Identity.Name.Equals("cozinha")
            ? "IndexCozinha" : "IndexGarcom";

        public ComandaController(INotificationHandler<DomainNotification> notifications,
                                  IProdutoAppService produtoAppService, 
                                  IMediatorHandler mediatorHandler, 
                                  IComandaAppService comandaAppService,
                                  IHttpContextAccessor httpContextAccessor
            ) : base(notifications, mediatorHandler, httpContextAccessor)
        {
            _produtoAppService = produtoAppService;
            _comandaAppService = comandaAppService;
        }

        [Route("vizualizar-comanda/garcom")]
        public async Task<IActionResult> IndexGarcom()
        {
            var model = await _comandaAppService.ObterComandaCliente(ClienteId);

            if(model == null)
                model = new ComandaViewModel { ClienteId = ClienteId };

            return View(model);
        }

        [Route("vizualizar-comanda/cozinha")]
        public async Task<IActionResult> IndexCozinha()
        {
            var model = await _comandaAppService.ObterComandaCliente(ClienteId);
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

            await _comandaAppService
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

            await _comandaAppService
                    .RemoverItemComanda(new RemoverItemComandaDTO
                    {
                        ClienteId = ClienteId,
                        ProdutoId = id,
                    });

            if (OperacaoValida())
            {
                return RedirectToAction(returnView);
            }
            
            return View("Index", await _comandaAppService.ObterComandaCliente(ClienteId));
        }

        [HttpPost]
        [Route("atualizar-item")]
        public async Task<IActionResult> AtualizarItem(Guid id, int quantidade, ItemStatus itemStatus)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            await _comandaAppService
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

            return View("Index", await _comandaAppService.ObterComandaCliente(ClienteId));
        }

        [Route("resumo-da-compra/finalizar")]
        public async Task<IActionResult> ResumoDaCompra()
        {
            return View(await _comandaAppService.ObterComandaCliente(ClienteId));
        }

        [Route("resumo-da-compra/cancelar")]
        public async Task<IActionResult> ResumoDaCompraCancelar()
        {
            return View(await _comandaAppService.ObterComandaCliente(ClienteId));
        }

        [HttpPost]
        [Route("finalizar-comanda")]
        public async Task<IActionResult> FinalizarComanda(ComandaViewModel comandaViewModel)
        {
            var carrinho = await _comandaAppService.ObterComandaCliente(ClienteId);

            await _comandaAppService
                    .FinalizarComanda(new FinalizarComandaDTO
                    {

                        ComandaId = carrinho.ComandaId,
                        ClienteId = ClienteId

                    });

            if (OperacaoValida())
            {
                 return RedirectToAction("Index", "Vitrine");
             }

             return View("ResumoDaCompra", await _comandaAppService.ObterComandaCliente(ClienteId));
        }

        [HttpPost]
        [Route("cancelar-comanda")]
        public async Task<IActionResult> CancelarComanda(ComandaViewModel comandaViewModel)
        {
            var carrinho = await _comandaAppService.ObterComandaCliente(ClienteId);

            await _comandaAppService
                    .CancelarComanda(new CancelarComandaDTO
                    {
                        ComandaId = carrinho.ComandaId,
                        ClienteId = ClienteId
                    });

            if (OperacaoValida())
            {
                return RedirectToAction("Index", "Vitrine");
            }

            return View("ResumoDaCompraCancelar", await _comandaAppService.ObterComandaCliente(ClienteId));
        }
    }
}