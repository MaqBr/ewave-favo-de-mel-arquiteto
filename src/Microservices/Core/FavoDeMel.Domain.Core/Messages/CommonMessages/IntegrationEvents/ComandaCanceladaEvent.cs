using System;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents
{
    public class ComandaCanceladaEvent : IntegrationEvent
    {
        public Guid ComandaId { get; private set; }

        public ComandaCanceladaEvent(Guid comandaId)
        {
            ComandaId = comandaId;
            AggregateId = comandaId;
        }
    }
}