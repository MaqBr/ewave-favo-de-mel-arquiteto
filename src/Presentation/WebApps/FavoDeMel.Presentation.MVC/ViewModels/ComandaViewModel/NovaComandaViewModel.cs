using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.ViewModels.ComandaViewModel
{
    public class NovaComandaViewModel
    {
        public Guid Id { get; set; }
        public Guid MesaId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Codigo { get; set; }
        public int Numero { get; set; }
    }
}
