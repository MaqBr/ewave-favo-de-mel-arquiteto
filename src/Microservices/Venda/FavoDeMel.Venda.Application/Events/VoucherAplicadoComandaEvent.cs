using System;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Venda.Application.Events
{
    public class VoucherAplicadoComandaEvent : Event
    {
        public Guid ClienteId { get; private set; }
        public Guid ComandaId { get; private set; }
        public Guid VoucherId { get; private set; }

        public VoucherAplicadoComandaEvent(Guid clienteId, Guid comandaId, Guid voucherId)
        {
            AggregateId = comandaId;
            ClienteId = clienteId;
            ComandaId = comandaId;
            VoucherId = voucherId;
        }
    }
}