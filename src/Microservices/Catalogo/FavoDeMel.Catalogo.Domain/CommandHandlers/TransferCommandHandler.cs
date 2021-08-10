using BuildingBlocks.EventBus.Abstractions;
using MediatR;
using FavoDeMel.Catalogo.Domain.Commands;
using FavoDeMel.Catalogo.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Domain.CommandHandlers
{
    public class VendaCommandHandler : IRequestHandler<CreateVendaCommand, bool>
    {
        private readonly IEventBus _bus;

        public VendaCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(CreateVendaCommand request, CancellationToken cancellationToken)
        {
            //publish event to RabbitMQ
            _bus.Publish(new VendaCreatedEvent(request.From, request.To, request.Amount));

            return Task.FromResult(true);
        }
    }
}
