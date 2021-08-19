using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Venda.Domain.Models
{
    public class AdicionarComandaDTO
    {
        public Guid MesaId { get; set; }
        public string Codigo { get; set; }
    }
}
