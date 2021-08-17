using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.IntegrationEvents.EventHandling
{
    public class PedidoProcessamentoCanceladoEventHandler : IIntegrationEventHandler<ComandaProcessamentoCanceladoEvent>
    {
        public Task Handle(ComandaProcessamentoCanceladoEvent @event)
        {
            var pedidoProcessamentoCancelado = @event;
            
            //TODO: implementação após ACK
            return Task.CompletedTask;
        }
    }
}
