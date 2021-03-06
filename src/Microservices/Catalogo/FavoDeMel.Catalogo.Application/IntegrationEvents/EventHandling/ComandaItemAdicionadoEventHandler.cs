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

            //TODO: implementar notificação em hub SignalR
            return Task.CompletedTask;
        }
    }
}
