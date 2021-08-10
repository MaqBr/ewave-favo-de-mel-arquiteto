﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FavoDeMel.Catalogo.Domain.Commands
{
    public class CreateTransferCommand : TransferCommand
    {
        public CreateTransferCommand(int from, int to, decimal amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }
    }
}
