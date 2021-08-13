using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.IntegrationEvents.EventHandling
{
    public class PedidoFinalizadoEventHandler : IIntegrationEventHandler<PedidoFinalizadoEvent>
    {
        public Task Handle(PedidoFinalizadoEvent @event)
        {
            var pedidoFinalizado = @event;
            
            //TODO: implementação após ACK
            return Task.CompletedTask;
        }
    }
}
