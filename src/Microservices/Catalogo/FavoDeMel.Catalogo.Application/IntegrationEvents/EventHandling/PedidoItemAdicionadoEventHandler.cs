using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.IntegrationEvents.EventHandling
{
    public class PedidoItemAdicionadoEventHandler : IIntegrationEventHandler<PedidoItemAdicionadoEvent>
    {
        public Task Handle(PedidoItemAdicionadoEvent @event)
        {
            var itemAdicionado = @event;
            
            //TODO: implementação após ACK
            return Task.CompletedTask;
        }
    }
}
