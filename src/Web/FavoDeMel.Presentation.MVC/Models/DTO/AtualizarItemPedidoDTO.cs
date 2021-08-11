using System;

namespace FavoDeMel.Presentation.MVC.Models.DTO
{
    public class AtualizarItemPedidoDTO
    {
        public Guid ClienteId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
