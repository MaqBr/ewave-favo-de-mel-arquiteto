using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Domain.Core.Messages.CommonMessages.Notifications;
using FavoDeMel.Presentation.MVC.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FavoDeMel.Presentation.MVC.Controllers
{
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoAppService _pedidoAppService;

        public PedidoController(
            IPedidoAppService pedidoAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IHttpContextAccessor httpContextAccessor) : base(notifications, mediatorHandler, httpContextAccessor)
        {
            _pedidoAppService = pedidoAppService;
        }

        [Route("meus-pedidos")]
        public async Task<IActionResult> Index()
        {
            return View(await _pedidoAppService.ObterPedidosCliente(ClienteId));
        }
    }
}