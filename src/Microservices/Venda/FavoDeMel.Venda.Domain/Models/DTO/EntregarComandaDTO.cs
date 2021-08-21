using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Venda.Domain.Models
{
    public class EntregarComandaDTO
    {
        public Guid ComandaId { get; set; }
        public Guid MesaId { get; set; }
        public decimal Total { get; set; }
    }
}
