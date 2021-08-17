using System;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Venda.Application
{
    public class CancelarProcessamentoComandaEstornarEstoqueCommand : Command
    {
        public Guid ComandaId { get; private set; }
        public Guid ClienteId { get; private set; }

        public CancelarProcessamentoComandaEstornarEstoqueCommand(Guid comandaId, Guid clienteId)
        {
            AggregateId = comandaId;
            ComandaId = comandaId;
            ClienteId = clienteId;
        }
    }
}