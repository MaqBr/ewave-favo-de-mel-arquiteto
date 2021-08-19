using System;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Venda.Application
{
    public class CancelarProcessamentoComandaEstornarEstoqueCommand : Command
    {
        public Guid ComandaId { get; private set; }
        public Guid MesaId { get; private set; }

        public CancelarProcessamentoComandaEstornarEstoqueCommand(Guid comandaId, Guid mesaId)
        {
            AggregateId = comandaId;
            ComandaId = comandaId;
            MesaId = mesaId;
        }
    }
}