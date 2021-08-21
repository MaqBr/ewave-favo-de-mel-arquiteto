using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.CommonMessages.Notifications;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Domain.Core.Messages.CommonMessages.Notifications;
using FavoDeMel.Presentation.MVC.Bussiness;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FavoDeMel.Presentation.MVC.Controllers
{
    [Authorize]
    public abstract class ControllerBase : Controller
    {
        public readonly IUser AppUser;
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }
        protected ControllerBase(INotificationHandler<DomainNotification> notifications, 
                                 IMediatorHandler mediatorHandler, IHttpContextAccessor httpContextAccessor, IUser appUser)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
            _httpContextAccessor = httpContextAccessor;
            AppUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UsuarioId = appUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao();
        }

        protected IEnumerable<string> ObterMensagensErro()
        {
            return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }
    }
}