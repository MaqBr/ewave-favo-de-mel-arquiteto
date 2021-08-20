using System;
using FavoDeMel.Domain.Core.DomainObjects.DTO;

namespace FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents
{
    public class ComandaIniciadaEvent : IntegrationEvent
    {
        public Guid ComandaId { get; private set; }
        public Guid MesaId { get; private set; }
        public decimal Total { get; private set; }
        public ListaProdutosComanda ProdutosComanda { get; private set; }

        public ComandaIniciadaEvent(Guid comandaId, Guid mesaId, ListaProdutosComanda itens, decimal total)
        {
            AggregateId = comandaId;
            ComandaId = comandaId;
            MesaId = mesaId;
            ProdutosComanda = itens;
            Total = total;
        }
    }
}