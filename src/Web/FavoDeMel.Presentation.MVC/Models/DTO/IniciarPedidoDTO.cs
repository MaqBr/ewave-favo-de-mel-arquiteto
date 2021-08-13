﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.Models.DTO
{
    public class IniciarPedidoDTO
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
        public decimal Total { get; set; }
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }
    }

    public class FinalizarPedidoDTO
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
    }

    public class CancelarPedidoDTO
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
    }
}
