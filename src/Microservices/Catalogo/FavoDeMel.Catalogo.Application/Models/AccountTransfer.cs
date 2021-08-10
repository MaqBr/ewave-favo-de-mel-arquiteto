using System;
using System.Collections.Generic;
using System.Text;

namespace FavoDeMel.Catalogo.Application.Models
{
    public class AccountVenda
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal VendaAmount { get; set; }
    }
}
