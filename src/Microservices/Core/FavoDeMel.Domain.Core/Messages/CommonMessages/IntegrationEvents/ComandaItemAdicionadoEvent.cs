using System;

namespace FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents
{
    public class ComandaItemAdicionadoEvent : IntegrationEvent
    {
        public Guid MesaId { get; private set; }
        public Guid ComandaId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; set; }
        public decimal ValorUnitario { get; private set; }
        public int Quantidade { get; private set; }

        public ComandaItemAdicionadoEvent(Guid mesaId, Guid comandaId, Guid produtoId, string produtoNome, decimal valorUnitario, int quantidade)
        {
            AggregateId = comandaId;
            MesaId = mesaId;
            ComandaId = comandaId;
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            ValorUnitario = valorUnitario;
            Quantidade = quantidade;
        }
    }
}