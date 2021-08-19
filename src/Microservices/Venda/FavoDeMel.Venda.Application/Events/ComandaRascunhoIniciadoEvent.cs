using System;
using FavoDeMel.Domain.Core.Messages;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;

namespace FavoDeMel.Venda.Application.Events
{
    public class ComandaRascunhoIniciadoEvent : IntegrationEvent
    {
        public Guid MesaId { get; private set; }
        public Guid ComandaId { get; private set; }

        public ComandaRascunhoIniciadoEvent(Guid mesaId, Guid comandaId)
        {
            AggregateId = comandaId;
            MesaId = mesaId;
            ComandaId = comandaId;
        }
    }
}