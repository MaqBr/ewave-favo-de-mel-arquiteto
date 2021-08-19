using System;
using FavoDeMel.Domain.Core.DomainObjects.DTO;

namespace FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents
{
    public class ComandaProcessamentoCanceladoEvent : IntegrationEvent
    {
        public Guid ComandaId { get; private set; }
        public Guid MesaId { get; private set; }
        public ListaProdutosComanda ProdutosComanda { get; private set; }

        public ComandaProcessamentoCanceladoEvent(Guid comandaId, Guid mesaId, ListaProdutosComanda produtosComanda)
        {
            AggregateId = comandaId;
            ComandaId = comandaId;
            MesaId = mesaId;
            ProdutosComanda = produtosComanda;
        }
    }
}