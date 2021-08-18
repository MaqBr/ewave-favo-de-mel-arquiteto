using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.CommonMessages.Notifications;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Domain.Core.Messages.CommonMessages.Notifications;
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
       
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected Guid ClienteId = Guid.Parse("9faf55b5-5088-42dd-8c55-5f8698b8295c");

        protected ControllerBase(INotificationHandler<DomainNotification> notifications, 
                                 IMediatorHandler mediatorHandler, IHttpContextAccessor httpContextAccessor)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
            _httpContextAccessor = httpContextAccessor;
        }

        protected async Task<string> ObterTokenAutenticacao()
        {
            var token = await _httpContextAccessor
                .HttpContext.GetTokenAsync("access_token");

            return token;
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