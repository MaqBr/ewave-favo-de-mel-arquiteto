using System;
using FavoDeMel.Domain.Core.DomainObjects.DTO;

namespace FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents
{
    public class ComandaProcessamentoCanceladoEvent : IntegrationEvent
    {
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public ListaProdutosComanda ProdutosPedido { get; private set; }

        public ComandaProcessamentoCanceladoEvent(Guid pedidoId, Guid clienteId, ListaProdutosComanda produtosPedido)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            ClienteId = clienteId;
            ProdutosPedido = produtosPedido;
        }
    }
}