using System;
using System.Collections.Generic;

namespace FavoDeMel.Domain.Core.DomainObjects.DTO
{
    public class ListaProdutosComanda
    {
        public Guid ComandaId { get; set; }
        public ICollection<Item> Itens { get; set; }
    }

    public class Item
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }
    }
}