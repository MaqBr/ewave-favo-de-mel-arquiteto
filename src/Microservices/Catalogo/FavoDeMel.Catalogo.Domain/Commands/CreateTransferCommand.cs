using System;
using System.Collections.Generic;
using System.Text;

namespace FavoDeMel.Catalogo.Domain.Commands
{
    public class CreateVendaCommand : VendaCommand
    {
        public CreateVendaCommand(int from, int to, decimal amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }
    }
}
