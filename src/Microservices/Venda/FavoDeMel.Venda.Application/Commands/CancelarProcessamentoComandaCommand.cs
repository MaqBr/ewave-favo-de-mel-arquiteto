using System;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Venda.Application
{
    public class CancelarProcessamentoComandaCommand : Command
    {
        public Guid ComandaId { get; private set; }
        public Guid MesaId { get; private set; }

        public CancelarProcessamentoComandaCommand(Guid comandaId, Guid mesaId)
        {
            AggregateId = comandaId;
            ComandaId = comandaId;
            MesaId = mesaId;
        }
    }
}