using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;

namespace FavoDeMel.Catalogo.Domain.Events
{
    public class TransferCreatedEvent : IntegrationEvent
    {
        public int From { get; private set; }
        public int To { get; private set; }
        public decimal Amount { get; private set; }

        public TransferCreatedEvent(int from, int to, decimal amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }
    }
}
