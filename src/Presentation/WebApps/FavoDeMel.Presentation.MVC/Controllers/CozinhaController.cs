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
using FavoDeMel.Presentation.MVC.ViewModels.VendaViewModels.Enuns;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FavoDeMel.Presentation.MVC.Controllers
{
    [ClaimsAuthorize("Cozinha", "Administrador")]
    public class CozinhaController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IComandaAppService _comandaAppService;

        public CozinhaController(INotificationHandler<DomainNotification> notifications,
                                  IProdutoAppService produtoAppService,
                                  IComandaAppService comandaAppService,
                                  IMediatorHandler mediatorHandler, 
                                  IHttpContextAccessor httpContextAccessor,
                                  IUser user
            ) : base(notifications, mediatorHandler, httpContextAccessor, user)
        {
            _produtoAppService = produtoAppService;
            _comandaAppService = comandaAppService;
        }

        [Route("dashborad")]
        public async Task<IActionResult> Dashboard(ComandaStatus status = ComandaStatus.Iniciado)
        {
            var model = await _comandaAppService.ObterComandasPorStatus(status);
            return View(new DashBoardComandaViewModel { Status = status, Data = model });
        }

        [Route("vizualizar/mesa/{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var model = await _comandaAppService.ObterComandaMesa(id);

            if (model == null) return BadRequest();

            return View(model);
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
                return RedirectToAction("Index", "Cozinha", new { id = mesaId });
            }

            return View("Index", await _comandaAppService.ObterComandaMesa(mesaId));
        }


        [Route("comanda/cancelar/confirmar")]
        public async Task<IActionResult> ResumoDaCompraCancelar(Guid mesaId)
        {
            var model = await _comandaAppService.ObterComandaMesa(mesaId);
            return View(model);
        }

        [Route("comanda/cancelar")]
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
                return RedirectToAction("Dashboard", "Cozinha", new { id = mesaId });
            }

            return View("ResumoDaCompraCancelar", await _comandaAppService.ObterComandaMesa(mesaId));
        }

        [Route("comanda/entregar")]
        public async Task<IActionResult> EntregarComanda(Guid mesaId)
        {
            var comanda = await _comandaAppService.ObterComandaMesa(mesaId);

            await _comandaAppService
                    .EntregarComanda(new EntregarComandaDTO
                    {
                        ComandaId = comanda.ComandaId,
                        MesaId = mesaId
                    });

            if (OperacaoValida())
            {
                return RedirectToAction("Dashboard", "Cozinha", new { id = mesaId });
            }

            return View("Index", await _comandaAppService.ObterComandaMesa(mesaId));
        }

    }
}