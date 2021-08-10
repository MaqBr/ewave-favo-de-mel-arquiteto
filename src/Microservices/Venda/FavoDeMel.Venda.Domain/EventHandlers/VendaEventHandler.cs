using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Venda.Domain.Events;
using FavoDeMel.Venda.Domain.Interfaces;
using FavoDeMel.Venda.Domain.Models;
using System.Threading.Tasks;

namespace FavoDeMel.Venda.Domain.EventHandlers
{
    public class VendaEventHandler : IIntegrationEventHandler<VendaCreatedEvent>
    {
        private readonly IVendaRepository _VendaRepository;

        public VendaEventHandler(IVendaRepository VendaRepository)
        {
            _VendaRepository = VendaRepository;
        }

        public Task Handle(VendaCreatedEvent @event)
        {
            _VendaRepository.Add(new VendaLog()
            {
                FromAccount = @event.From,
                ToAccount = @event.To,
                VendaAmount = @event.Amount
            });

            return Task.CompletedTask;
        }
    }
}
