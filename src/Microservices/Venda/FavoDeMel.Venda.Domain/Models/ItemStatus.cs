﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Venda.Domain.Models
{
    public enum ItemStatus
    {
        Aberto = 1,
        EmPreparo = 2,
        Pronto = 3,
        Finalizado = 4,
        Cancelado = 5
    }
}
