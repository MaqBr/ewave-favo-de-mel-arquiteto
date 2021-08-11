using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FavoDeMel.Domain.Core.Communication.Mediator;

namespace FavoDeMel.Venda.Application.Events
{
    public class PedidoEventHandler :
        INotificationHandler<PedidoRascunhoIniciadoEvent>,
        INotificationHandler<PedidoItemAdicionadoEvent>
    {

        private readonly IMediatorHandler _mediatorHandler;

        public PedidoEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}