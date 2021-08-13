using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Venda.Domain.Models
{
    public class FinalizarPedidoDTO
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
    }
}
