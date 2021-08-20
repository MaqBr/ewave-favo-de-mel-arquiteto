using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.IntegrationEvents.EventHandling
{
    public class ComandaItemAdicionadoEventHandler : IIntegrationEventHandler<ComandaItemAdicionadoEvent>
    {
        public Task Handle(ComandaItemAdicionadoEvent @event)
        {
            var itemAdicionado = @event;

            //TODO: Atualizar entidade Produto -> EstoqueService -> DebitarItemEstoque(Guid produtoId, int quantidade)
            return Task.CompletedTask;
        }
    }
}
