using System;
using System.Collections.Generic;
using FavoDeMel.Presentation.MVC.ViewModels;

namespace FavoDeMel.Presentation.MVC.CatalogoViewModels.Venda.ViewModels
{
    public class ComandaViewModel
    {
        public Guid ComandaId { get; set; }
        public MesaViewModel Mesa { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorDesconto { get; set; }
        public string VoucherCodigo { get; set; }
        public ComandaStatus ComandaStatus { get; set; }
        public List<ComandaItemViewModel> Items { get; set; } = new List<ComandaItemViewModel>();
    }

}