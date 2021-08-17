using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FavoDeMel.Domain.Core.Communication.Mediator;

namespace FavoDeMel.Venda.Application.Events
{
    public class ComandaEventHandler :
        INotificationHandler<ComandaRascunhoIniciadoEvent>,
        INotificationHandler<ComandaProdutoAdicionadoEvent>
    {

        private readonly IMediatorHandler _mediatorHandler;

        public ComandaEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(ComandaRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ComandaProdutoAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}