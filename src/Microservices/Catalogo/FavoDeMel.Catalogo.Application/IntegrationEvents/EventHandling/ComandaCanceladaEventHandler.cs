using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.IntegrationEvents.EventHandling
{
    public class ComandaCanceladaEventHandler : IIntegrationEventHandler<ComandaCanceladaEvent>
    {
        public Task Handle(ComandaCanceladaEvent @event)
        {
            var comandaCancelado = @event;
            
            //TODO: implementação após ACK
            return Task.CompletedTask;
        }
    }
}
