using System;
using FavoDeMel.Domain.Core.Messages;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;

namespace FavoDeMel.Venda.Application.Events
{
    public class ComandaProdutoAdicionadoEvent : IntegrationEvent
    {
        public Guid MesaId { get; private set; }
        public Guid ComandaId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }

        public ComandaProdutoAdicionadoEvent(Guid mesaId, Guid comandaId, Guid produtoId, int quantidade)
        {
            AggregateId = comandaId;
            MesaId = mesaId;
            ComandaId = comandaId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }
    }
}