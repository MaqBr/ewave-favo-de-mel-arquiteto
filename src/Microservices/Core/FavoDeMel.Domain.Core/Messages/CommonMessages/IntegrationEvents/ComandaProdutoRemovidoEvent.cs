using System;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents
{
    public class ComandaProdutoRemovidoEvent : IntegrationEvent
    {
        public Guid ClienteId { get; private set; }
        public Guid ComandaId { get; private set; }
        public Guid ProdutoId { get; private set; }

        public ComandaProdutoRemovidoEvent(Guid clienteId, Guid comandaId, Guid produtoId)
        {
            AggregateId = comandaId;
            ClienteId = clienteId;
            ComandaId = comandaId;
            ProdutoId = produtoId;
        }
    }
}