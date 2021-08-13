using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.IntegrationEvents.EventHandling
{
    public class PedidoProdutoRemovidoEventHandler : IIntegrationEventHandler<PedidoProdutoRemovidoEvent>
    {
        public Task Handle(PedidoProdutoRemovidoEvent @event)
        {
            var itemRemovido = @event;
            
            //TODO: implementação após ACK
            return Task.CompletedTask;
        }
    }
}
