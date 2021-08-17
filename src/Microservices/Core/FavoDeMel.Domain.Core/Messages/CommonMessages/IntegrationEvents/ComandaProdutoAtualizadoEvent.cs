using System;
using FavoDeMel.Domain.Core.DomainObjects;
using FavoDeMel.Domain.Core.Messages;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;

namespace FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents
{
    public class ComandaProdutoAtualizadoEvent : IntegrationEvent
    {
        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public ItemStatus ItemStatus { get; set; }

        public ComandaProdutoAtualizadoEvent(Guid clienteId, Guid pedidoId, Guid produtoId, int quantidade, ItemStatus itemStatus)
        {
            AggregateId = pedidoId;
            ClienteId = clienteId;
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            ItemStatus = itemStatus;
        }
    }
}