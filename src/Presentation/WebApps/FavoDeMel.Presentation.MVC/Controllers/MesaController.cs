using System;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Domain.Core.Messages.CommonMessages.Notifications;
using FavoDeMel.Presentation.MVC.Services;
using FavoDeMel.Presentation.MVC.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FavoDeMel.Presentation.MVC.Controllers
{
    public class MesaController : ControllerBase
    {
        private readonly IMesaAppService _mesaAppService;

        public MesaController(INotificationHandler<DomainNotification> notifications, 
                                  IMediatorHandler mediatorHandler,
                                  IHttpContextAccessor httpContextAccessor,
                                  IMesaAppService mesaAppService)
            : base(notifications, mediatorHandler, httpContextAccessor)
        {
            _mesaAppService = mesaAppService;
        }

        [Route("")]
        [Route("listar")]
        public async Task<IActionResult> Index()
        {
            
            var mesas = await _mesaAppService.ObterTodos();
            var vm = new IndexViewModel { Mesas = mesas };
            return View(vm);
        }

        [Route("obter/{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var vm = await _mesaAppService.ObterPorId(id);
            return View(vm);
        }

    }
}