using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.IntegrationEvents.EventHandling
{
    public class ComandaFinalizadaEventHandler : IIntegrationEventHandler<ComandaFinalizadaEvent>
    {
        public Task Handle(ComandaFinalizadaEvent @event)
        {
            var comandaFinalizada = @event;
            
            //TODO: implementação após ACK
            return Task.CompletedTask;
        }
    }
}
