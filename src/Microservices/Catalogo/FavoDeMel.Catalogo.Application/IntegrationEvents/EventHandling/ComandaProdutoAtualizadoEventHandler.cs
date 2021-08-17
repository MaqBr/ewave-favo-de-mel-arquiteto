using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.IntegrationEvents.EventHandling
{
    public class ComandaProdutoAtualizadoEventHandler : IIntegrationEventHandler<ComandaProdutoAtualizadoEvent>
    {
        public Task Handle(ComandaProdutoAtualizadoEvent @event)
        {
            var comandaProdutoAtualizado = @event;
            
            //TODO: implementação após ACK
            return Task.CompletedTask;
        }
    }
}
