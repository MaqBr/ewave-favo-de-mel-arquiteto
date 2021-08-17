using System;
using FavoDeMel.Domain.Core.Messages;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;

namespace FavoDeMel.Venda.Application.Events
{
    public class ComandaRascunhoIniciadoEvent : IntegrationEvent
    {
        public Guid ClienteId { get; private set; }
        public Guid ComandaId { get; private set; }

        public ComandaRascunhoIniciadoEvent(Guid clienteId, Guid comandaId)
        {
            AggregateId = comandaId;
            ClienteId = clienteId;
            ComandaId = comandaId;
        }
    }
}