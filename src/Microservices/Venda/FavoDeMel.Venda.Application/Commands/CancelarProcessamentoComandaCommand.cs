using System;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Venda.Application
{
    public class CancelarProcessamentoComandaCommand : Command
    {
        public Guid ComandaId { get; private set; }
        public Guid ClienteId { get; private set; }

        public CancelarProcessamentoComandaCommand(Guid pedidoId, Guid clienteId)
        {
            AggregateId = pedidoId;
            ComandaId = pedidoId;
            ClienteId = clienteId;
        }
    }
}