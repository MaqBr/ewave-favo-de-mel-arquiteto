using System;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Venda.Application
{
    public class CancelarProcessamentoPedidoEstornarEstoqueCommand : Command
    {
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }

        public CancelarProcessamentoPedidoEstornarEstoqueCommand(Guid pedidoId, Guid clienteId)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            ClienteId = clienteId;
        }
    }
}