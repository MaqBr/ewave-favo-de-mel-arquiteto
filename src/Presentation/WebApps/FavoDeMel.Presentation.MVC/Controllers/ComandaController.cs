using System;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Domain.Core.Messages.CommonMessages.Notifications;
using FavoDeMel.Presentation.MVC.CatalogoViewModels.Venda.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using FavoDeMel.Presentation.MVC.Services;
using FavoDeMel.Presentation.MVC.ViewModels.ComandaViewModel;
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
        private readonly IMesaAppService _mesaAppService;
        private string returnView => User.Identity.IsAuthenticated && User.Identity.Name.Equals("cozinha")
            ? "IndexCozinha" : "IndexGarcom";

        public ComandaController(INotificationHandler<DomainNotification> notifications,
                                  IProdutoAppService produtoAppService, 
                                  IMediatorHandler mediatorHandler, 
                                  IComandaAppService comandaAppService,
                                  IMesaAppService mesaAppService,
                                  IHttpContextAccessor httpContextAccessor
            ) : base(notifications, mediatorHandler, httpContextAccessor)
        {
            _produtoAppService = produtoAppService;
            _comandaAppService = comandaAppService;
            _mesaAppService = mesaAppService;
        }

        [Route("vizualizar-comanda/mesa/{id}")]
        public async Task<IActionResult> IndexComandaMesa(Guid id)
        {
            var model = await _comandaAppService.ObterComandaMesa(id);

            if (model == null) return BadRequest();

            return View(model);
        }


        [Route("vizualizar-comanda/garcom")]
        public async Task<IActionResult> IndexGarcom(Guid mesaId)
        {
            var model = await _comandaAppService.ObterComandaMesa(mesaId);
            
            if (model == null) return BadRequest();

            var mesa = await _mesaAppService.ObterPorId(mesaId);
            model.Mesa = mesa;

            return View(model);
        }

        [Route("vizualizar-comanda/cozinha")]
        public async Task<IActionResult> IndexCozinha(Guid mesaId)
        {
            var model = await _comandaAppService.ObterComandaMesa(mesaId);
            if (model == null) return BadRequest();

            var mesa = await _mesaAppService.ObterPorId(mesaId);
            model.Mesa = mesa;

            return View(model);
        }

        [HttpGet]
        [Route("nova-comanda")]
        public async Task<IActionResult> NovaComanda(Guid id)
        {
            var mesa = await _mesaAppService.ObterPorId(id);
            var vm = new NovaComandaViewModel()
            {
                MesaId = mesa.MesaId,
                Numero = mesa.Numero
            };

            return View(vm);
        }

        [HttpPost]
        [Route("comanda/adicionar")]
        public async Task<IActionResult> AdicionarComanda(Guid id, string codigo)
        {

            if (codigo == null) return BadRequest();

            await _comandaAppService.AdicionarComanda(new AdicionarComandaDTO { 
            
                MesaId = id,
                Codigo = codigo
            
            });

            if (OperacaoValida())
            {
                return RedirectToAction("Index", "Mesa");
            }

            TempData["Erros"] = ObterMensagensErro();
            return RedirectToAction("Index", "Mesa");
        }


        [HttpPost]
        [Route("comanda/item/adicionar")]
        public async Task<IActionResult> AdicionarItem(Guid id, Guid mesaId, int quantidade)
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
                    MesaId = mesaId, 
                    ProdutoId = produto.Id, 
                    Nome = produto.Nome, 
                    Quantidade = quantidade, 
                    ValorUnitario = produto.Valor 
                });

            if (OperacaoValida())
            {
                return RedirectToAction("Index", "Vitrine", new { mesaId });
            }

            TempData["Erros"] = ObterMensagensErro();
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { mesaId });
        }

        [HttpPost]
        [Route("comanda/item/remover")]
        public async Task<IActionResult> RemoverItem(Guid id, Guid mesaId)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            await _comandaAppService
                    .RemoverItemComanda(new RemoverItemComandaDTO
                    {
                        MesaId = mesaId,
                        ProdutoId = id,
                    });

            if (OperacaoValida())
            {
                return RedirectToAction(returnView, new { mesaId });
            }
            
            return View("Index", await _comandaAppService.ObterComandaMesa(mesaId));
        }

        [HttpPost]
        [Route("comanda/item/atualizar")]
        public async Task<IActionResult> AtualizarItem(Guid id, Guid mesaId, int quantidade, ItemStatus itemStatus)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            await _comandaAppService
                    .AtualizarItemComanda(new AtualizarItemComandaDTO
                    {
                        MesaId = mesaId,
                        ProdutoId = produto.Id,
                        Quantidade = quantidade,
                        ItemStatus = itemStatus
                    });

            if (OperacaoValida())
            {
                return RedirectToAction(returnView, new { mesaId });
            }

            return View("Index", await _comandaAppService.ObterComandaMesa(mesaId));
        }

        [Route("resumo-da-compra/finalizar")]
        public async Task<IActionResult> ResumoDaCompra(Guid mesaId)
        {
            return View(await _comandaAppService.ObterComandaMesa(mesaId));
        }

        [Route("resumo-da-compra/cancelar")]
        public async Task<IActionResult> ResumoDaCompraCancelar(Guid mesaId)
        {
            return View(await _comandaAppService.ObterComandaMesa(mesaId));
        }

        [HttpPost]
        [Route("finalizar-comanda")]
        public async Task<IActionResult> FinalizarComanda(ComandaViewModel comandaViewModel)
        {
            var comanda = await _comandaAppService.ObterComandaMesa(comandaViewModel.Mesa.MesaId);

            await _comandaAppService
                    .FinalizarComanda(new FinalizarComandaDTO
                    {

                        ComandaId = comanda.ComandaId,
                        MesaId = comanda.Mesa.MesaId

                    });

            if (OperacaoValida())
            {
                 return RedirectToAction("Index", "Vitrine", new { comanda.Mesa.MesaId });
             }

             return View("ResumoDaCompra", await _comandaAppService.ObterComandaMesa(comandaViewModel.Mesa.MesaId));
        }

        [HttpPost]
        [Route("cancelar-comanda")]
        public async Task<IActionResult> CancelarComanda(ComandaViewModel comandaViewModel)
        {
            var carrinho = await _comandaAppService.ObterComandaMesa(comandaViewModel.Mesa.MesaId);

            await _comandaAppService
                    .CancelarComanda(new CancelarComandaDTO
                    {
                        ComandaId = carrinho.ComandaId,
                        MesaId = comandaViewModel.Mesa.MesaId
                    });

            if (OperacaoValida())
            {
                return RedirectToAction("Index", "Vitrine", new { comandaViewModel.Mesa.MesaId });
            }

            return View("ResumoDaCompraCancelar", await _comandaAppService.ObterComandaMesa(comandaViewModel.Mesa.MesaId));
        }
    }
}