using System;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents
{
    public class ComandaProdutoRemovidoEvent : IntegrationEvent
    {
        public Guid MesaId { get; private set; }
        public Guid ComandaId { get; private set; }
        public Guid ProdutoId { get; private set; }

        public ComandaProdutoRemovidoEvent(Guid mesaId, Guid comandaId, Guid produtoId)
        {
            AggregateId = comandaId;
            MesaId = mesaId;
            ComandaId = comandaId;
            ProdutoId = produtoId;
        }
    }
}