using System;

namespace FavoDeMel.Presentation.MVC.Models.DTO
{
    public class AdicionarItemPedidoDTO
    {
        public Guid ClienteId { get; set; }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

    }
}
