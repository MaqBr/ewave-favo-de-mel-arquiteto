using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.IntegrationEvents.EventHandling
{
    public class PedidoCanceladoEventHandler : IIntegrationEventHandler<PedidoCanceladoEvent>
    {
        public Task Handle(PedidoCanceladoEvent @event)
        {
            var pedidoCancelado = @event;
            
            //TODO: implementação após ACK
            return Task.CompletedTask;
        }
    }
}
