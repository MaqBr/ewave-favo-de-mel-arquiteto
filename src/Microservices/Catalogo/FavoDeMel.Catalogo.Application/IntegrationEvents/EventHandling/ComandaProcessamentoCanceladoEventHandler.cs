using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.IntegrationEvents.EventHandling
{
    public class ComandaProcessamentoCanceladoEventHandler : IIntegrationEventHandler<ComandaProcessamentoCanceladoEvent>
    {
        public Task Handle(ComandaProcessamentoCanceladoEvent @event)
        {
            var comandaProcessamentoCancelado = @event;

            //TODO: implementar notificação em hub SignalR
            return Task.CompletedTask;
        }
    }
}
