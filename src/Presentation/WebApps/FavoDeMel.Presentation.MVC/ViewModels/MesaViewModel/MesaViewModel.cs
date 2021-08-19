using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.ViewModels.MesaViewModel
{
    public class MesaViewModel
    {
        public Guid MesaId { get; set; }
        public int Numero { get; set; }
        public DateTime DataCriacao { get; set; }
        public SituacaoMesa Situacao { get; set; }
    }

    public enum SituacaoMesa
    {
        Livre = 1,
        Ocupada = 2
    }
}
