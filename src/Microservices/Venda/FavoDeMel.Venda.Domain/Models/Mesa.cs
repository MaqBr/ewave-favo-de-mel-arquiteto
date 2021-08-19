using FavoDeMel.Domain.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Venda.Domain.Models
{
    public class Mesa : Entity, IAggregateRoot
    {
        public int Numero { get; set; }
        public DateTime DataCriacao { get; set; }
        public SituacaoMesa Situacao { get; set; }
        public ICollection<Comanda> Comandas { get; set; }
    }
}
