using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.IntegrationEvents.EventHandling
{
    public class ComandaAdicionadaEventHandler : IIntegrationEventHandler<ComandaAdicionadaEvent>
    {
        public Task Handle(ComandaAdicionadaEvent @event)
        {
            var comandaAdicionada = @event;
            
            //TODO: implementar notificação em hub SignalR
            return Task.CompletedTask;
        }
    }
}
