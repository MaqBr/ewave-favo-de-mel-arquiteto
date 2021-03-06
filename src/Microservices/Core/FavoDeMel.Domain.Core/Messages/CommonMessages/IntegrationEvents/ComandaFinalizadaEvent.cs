using System;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents
{
    public class ComandaFinalizadaEvent : IntegrationEvent
    {
        public Guid ComandaId { get; private set; }

        public ComandaFinalizadaEvent(Guid comandaId)
        {
            ComandaId = comandaId;
            AggregateId = comandaId;
        }
    }
}