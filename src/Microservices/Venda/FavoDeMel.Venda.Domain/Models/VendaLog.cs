using FavoDeMel.Domain.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace FavoDeMel.Venda.Domain.Models
{
    public class VendaLog : Entity, IAggregateRoot
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal VendaAmount { get; set; }
    }
}
