using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.IntegrationEvents.EventHandling
{
    public class PedidoProdutoAtualizadoEventHandler : IIntegrationEventHandler<PedidoProdutoAtualizadoEvent>
    {
        public Task Handle(PedidoProdutoAtualizadoEvent @event)
        {
            var pedidoProdutoAtualizado = @event;
            
            //TODO: implementação após ACK
            return Task.CompletedTask;
        }
    }
}
