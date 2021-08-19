using System;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents
{
    public class ComandaAdicionadaEvent : IntegrationEvent
    {
        public Guid ComandaId { get; private set; }

        public ComandaAdicionadaEvent(Guid comandaId)
        {
            ComandaId = comandaId;
            AggregateId = comandaId;
        }
    }
}