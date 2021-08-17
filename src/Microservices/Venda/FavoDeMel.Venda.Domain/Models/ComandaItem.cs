using System;
using FavoDeMel.Domain.Core.DomainObjects;

namespace FavoDeMel.Venda.Domain.Models
{
    public class ComandaItem : Entity
    {
        public Guid ComandaId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public ItemStatus ItemStatus { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        // EF Rel.
        public Comanda Comanda { get; set; }

        public ComandaItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario, ItemStatus itemStatus = ItemStatus.Aberto)
        {
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            ItemStatus = itemStatus;
        }

        protected ComandaItem() { }

        internal void AssociarPedido(Guid comandaId)
        {
            ComandaId = comandaId;
        }

        public decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }

        internal void AtualizarUnidades(int unidades)
        {
            Quantidade = unidades;
        }

        internal void AtualizarItemStatus(ItemStatus itemStatus)
        {
            ItemStatus = itemStatus;
        }

        public override bool EhValido()
        {
            return true;
        }
    }
}