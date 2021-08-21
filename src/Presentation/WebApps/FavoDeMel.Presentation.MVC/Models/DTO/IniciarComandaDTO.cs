using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.Models.DTO
{
    public class IniciarComandaDTO
    {
        public Guid ComandaId { get; set; }
        public Guid MesaId { get; set; }
        public decimal Total { get; set; }
    }

    public class EntregarComandaDTO
    {
        public Guid ComandaId { get; set; }
        public Guid MesaId { get; set; }
        public decimal Total { get; set; }
    }

    public class FinalizarComandaDTO
    {
        public Guid ComandaId { get; set; }
        public Guid MesaId { get; set; }
    }

    public class CancelarComandaDTO
    {
        public Guid ComandaId { get; set; }
        public Guid MesaId { get; set; }
    }
}
