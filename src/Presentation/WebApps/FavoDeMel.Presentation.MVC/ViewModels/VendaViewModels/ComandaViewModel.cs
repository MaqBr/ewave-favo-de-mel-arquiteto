using FavoDeMel.Presentation.MVC.ViewModels.VendaViewModels.Enuns;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FavoDeMel.Presentation.MVC.CatalogoViewModels.Venda.ViewModels
{
    public class ComandaViewModel
    {
        public Guid ComandaId { get; set; }
        public Guid ClienteId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorDesconto { get; set; }
        public string VoucherCodigo { get; set; }
        public ItemStatus ItensStatus { get; set; }
        public List<ComandaItemViewModel> Items { get; set; } = new List<ComandaItemViewModel>();
    }

}