using System;

namespace FavoDeMel.Presentation.MVC.CatalogoViewModels.Venda.ViewModels
{
    public class PedidoViewModel
    {
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCadastro { get; set; }
        public int PedidoStatus { get; set; }
    }
}