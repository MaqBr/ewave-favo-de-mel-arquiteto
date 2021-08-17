using FavoDeMel.Domain.Core.DomainObjects;
using System;

namespace FavoDeMel.Venda.Domain.Models
{
    public class AtualizarItemComandaDTO
    {
        public Guid ClienteId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public ItemStatus ItemStatus { get; set; }
    }
}
