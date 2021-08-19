using FavoDeMel.Presentation.MVC.ViewModels.VendaViewModels.Enuns;
using System;

namespace FavoDeMel.Presentation.MVC.Models.DTO
{
    public class AtualizarItemComandaDTO
    {
        public Guid MesaId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public ItemStatus ItemStatus { get; set; }
    }
}
