using System;
using FavoDeMel.Domain.Core.DomainObjects.DTO;

namespace FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents
{
    public class ComandaIniciadaEvent : IntegrationEvent
    {
        public Guid ComandaId { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal Total { get; private set; }
        public ListaProdutosComanda ProdutosComanda { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }

        public ComandaIniciadaEvent(Guid comandaId, Guid clienteId, ListaProdutosComanda itens, decimal total, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao)
        {
            AggregateId = comandaId;
            ComandaId = comandaId;
            ClienteId = clienteId;
            ProdutosComanda = itens;
            Total = total;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
        }
    }
}