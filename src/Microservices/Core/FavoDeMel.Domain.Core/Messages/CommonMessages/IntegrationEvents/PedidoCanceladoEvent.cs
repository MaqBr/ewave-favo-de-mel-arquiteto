﻿using System;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PedidoCanceladoEvent : IntegrationEvent
    {
        public Guid PedidoId { get; private set; }

        public PedidoCanceladoEvent(Guid pedidoId)
        {
            PedidoId = pedidoId;
            AggregateId = pedidoId;
        }
    }
}