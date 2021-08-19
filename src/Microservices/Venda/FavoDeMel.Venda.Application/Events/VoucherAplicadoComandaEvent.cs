using System;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Venda.Application.Events
{
    public class VoucherAplicadoComandaEvent : Event
    {
        public Guid MesaId { get; private set; }
        public Guid ComandaId { get; private set; }
        public Guid VoucherId { get; private set; }

        public VoucherAplicadoComandaEvent(Guid mesaId, Guid comandaId, Guid voucherId)
        {
            AggregateId = comandaId;
            MesaId = mesaId;
            ComandaId = comandaId;
            VoucherId = voucherId;
        }
    }
}