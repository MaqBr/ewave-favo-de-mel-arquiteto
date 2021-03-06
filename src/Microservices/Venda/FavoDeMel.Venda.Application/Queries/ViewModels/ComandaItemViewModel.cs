using FavoDeMel.Domain.Core.DomainObjects;
using System;

namespace FavoDeMel.Venda.Application.Queries.ViewModels
{
    public class ComandaItemViewModel
    {
        public Guid ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public ItemStatus ItemStatus { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }

    }
}