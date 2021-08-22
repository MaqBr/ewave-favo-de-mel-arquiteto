using System;
using System.Linq;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Domain.Core.Messages.CommonMessages.Notifications;
using FavoDeMel.Presentation.MVC.Bussiness;
using FavoDeMel.Presentation.MVC.Models.DTO;
using FavoDeMel.Presentation.MVC.Services;
using FavoDeMel.Presentation.MVC.Venda.ViewModels;
using FavoDeMel.Presentation.MVC.ViewModels;
using FavoDeMel.Presentation.MVC.ViewModels.ComandaViewModel;
using FavoDeMel.Presentation.MVC.ViewModels.VendaViewModels.Enuns;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FavoDeMel.Presentation.MVC.Controllers
{
    [ClaimsAuthorize("Atendimento", "Administrador")]
    public class ComandaController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IComandaAppService _comandaAppService;
        private readonly IMesaAppService _mesaAppService;

        public ComandaController(INotificationHandler<DomainNotification> notifications,
                                  IProdutoAppService produtoAppService, 
                                  IMediatorHandler mediatorHandler, 
                                  IComandaAppService comandaAppService,
                                  IMesaAppService mesaAppService,
                                  IHttpContextAccessor httpContextAccessor,
                                  IUser user
            ) : base(notifications, mediatorHandler, httpContextAccessor, user)
        {
            _produtoAppService = produtoAppService;
            _comandaAppService = comandaAppService;
            _mesaAppService = mesaAppService;
        }

        [Route("vizualizar-comanda/atendimento/dashborad")]
        public async Task<IActionResult> Dashboard(ComandaStatus status = ComandaStatus.Iniciado)
        {
            var model = await _comandaAppService.ObterComandasPorStatus(status);
            return View(new DashBoardComandaViewModel { Status = status, Data = model });
        }

        [Route("vizualizar-comanda/mesa/{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var model = await _comandaAppService.ObterComandaMesa(id);

            if (model == null) return BadRequest();

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
        public async Task<IActionResult> AdicionarComanda(NovaComandaViewModel comanda)
        {
            if (ModelState.IsValid)
            {
                await _comandaAppService.AdicionarComanda(new AdicionarComandaDTO
                {

                    MesaId = comanda.Id,
                    Codigo = comanda.Codigo

                });

                if (OperacaoValida())
                {
                    return RedirectToAction("Index", "Mesa");
                }

                TempData["Erros"] = ObterMensagensErro();
                return RedirectToAction("Index", "Mesa");
            }
            else
            {
                ModelState.AddModelError("", "Os dados informados são inválidos");
            }
            comanda.MesaId = comanda.Id;
            return View("NovaComanda", comanda);
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
                return RedirectToAction("Index", "Comanda", new { id = mesaId });
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
                return RedirectToAction("Index", "Comanda", new { id = mesaId });
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
                return RedirectToAction("Index", "Comanda", new { id = mesaId });
            }

            return View("Index", await _comandaAppService.ObterComandaMesa(mesaId));
        }

        [Route("resumo-da-compra/finalizar")]
        public async Task<IActionResult> ResumoDaCompra(Guid mesaId)
        {
            var model = await _comandaAppService.ObterComandaMesa(mesaId);

            if (!model.Items.Any()) {
                await _comandaAppService
                    .FinalizarComanda(new FinalizarComandaDTO
                    {

                        ComandaId = model.ComandaId,
                        MesaId = mesaId

                    });
                return RedirectToAction("Index", "Mesa", new { id = mesaId });
            }


            return View(model);
        }

        [Route("resumo-da-compra/cancelar")]
        public async Task<IActionResult> ResumoDaCompraCancelar(Guid mesaId)
        {
            var model = await _comandaAppService.ObterComandaMesa(mesaId);
            return View(model);
        }

        [Route("iniciar-comanda")]
        public async Task<IActionResult> IniciarComanda(Guid mesaId)
        {
            var comanda = await _comandaAppService.ObterComandaMesa(mesaId);

            await _comandaAppService
                    .IniciarComanda(new IniciarComandaDTO
                    {
                        ComandaId = comanda.ComandaId,
                        MesaId = mesaId
                    });

            if (OperacaoValida())
            {
                return RedirectToAction("Index", "Mesa", new { id = mesaId });
            }

            return View("ResumoDaCompra", await _comandaAppService.ObterComandaMesa(mesaId));
        }

        [Route("finalizar-comanda")]
        public async Task<IActionResult> FinalizarComanda(Guid mesaId)
        {
            var comanda = await _comandaAppService.ObterComandaMesa(mesaId);

            await _comandaAppService
                    .FinalizarComanda(new FinalizarComandaDTO
                    {

                        ComandaId = comanda.ComandaId,
                        MesaId = mesaId

                    });

            if (OperacaoValida())
            {
                return RedirectToAction("Index", "Mesa", new { id = mesaId });
            }

             return View("ResumoDaCompra", await _comandaAppService.ObterComandaMesa(mesaId));
        }

        [Route("cancelar-comanda")]
        public async Task<IActionResult> CancelarComanda(Guid mesaId)
        {
            var comanda = await _comandaAppService.ObterComandaMesa(mesaId);

            await _comandaAppService
                    .CancelarComanda(new CancelarComandaDTO
                    {
                        ComandaId = comanda.ComandaId,
                        MesaId = mesaId
                    });

            if (OperacaoValida())
            {
                return RedirectToAction("Index", "Mesa", new { id = mesaId });
            }

            return View("ResumoDaCompraCancelar", await _comandaAppService.ObterComandaMesa(mesaId));
        }
    }
}