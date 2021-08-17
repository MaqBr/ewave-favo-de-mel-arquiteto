using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.IntegrationEvents.EventHandling
{
    public class ComandaProdutoRemovidoEventHandler : IIntegrationEventHandler<ComandaProdutoRemovidoEvent>
    {
        public Task Handle(ComandaProdutoRemovidoEvent @event)
        {
            var itemRemovido = @event;
            
            //TODO: implementação após ACK
            return Task.CompletedTask;
        }
    }
}
