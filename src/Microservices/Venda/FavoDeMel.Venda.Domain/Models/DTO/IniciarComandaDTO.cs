using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Venda.Domain.Models
{
    public class IniciarComandaDTO
    {
        public Guid ComandaId { get; set; }
        public Guid ClienteId { get; set; }
        public decimal Total { get; set; }
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }
    }
}
