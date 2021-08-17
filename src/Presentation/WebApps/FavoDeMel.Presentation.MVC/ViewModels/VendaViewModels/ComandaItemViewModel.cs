using FavoDeMel.Presentation.MVC.ViewModels.VendaViewModels.Enuns;
using System;

namespace FavoDeMel.Presentation.MVC.CatalogoViewModels.Venda.ViewModels
{
    public class ComandaItemViewModel
    {
        public Guid ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public ItemStatus ItemStatus { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }

    }



}