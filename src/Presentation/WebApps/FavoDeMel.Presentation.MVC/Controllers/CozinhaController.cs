using System;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Domain.Core.Messages.CommonMessages.Notifications;
using FavoDeMel.Presentation.MVC.Bussiness;
using FavoDeMel.Presentation.MVC.Services;
using FavoDeMel.Presentation.MVC.Venda.ViewModels;
using FavoDeMel.Presentation.MVC.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FavoDeMel.Presentation.MVC.Controllers
{
    [ClaimsAuthorize("Cozinha", "Administrador")]
    public class CozinhaController : ControllerBase
    {
        private readonly ICozinhaAppService _cozinhaAppService;

        public CozinhaController(INotificationHandler<DomainNotification> notifications,
                                  ICozinhaAppService cozinhaAppService, 
                                  IMediatorHandler mediatorHandler, 
                                  IHttpContextAccessor httpContextAccessor,
                                  IUser user
            ) : base(notifications, mediatorHandler, httpContextAccessor, user)
        {
            _cozinhaAppService = cozinhaAppService;
        }

        [Route("vizualizar-comanda/cozinha/dashborad")]
        public async Task<IActionResult> Dashboard(ComandaStatus status = ComandaStatus.Iniciado)
        {
            var model = await _cozinhaAppService.ObterComandasPorStatus(status);
            return View(new DashBoardComandaViewModel { Status = status, Data = model });
        }

        [Route("vizualizar-comanda/cozinha/mesa/{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var model = await _cozinhaAppService.ObterComandaMesa(id);

            if (model == null) return BadRequest();

            return View(model);
        }
    }
}