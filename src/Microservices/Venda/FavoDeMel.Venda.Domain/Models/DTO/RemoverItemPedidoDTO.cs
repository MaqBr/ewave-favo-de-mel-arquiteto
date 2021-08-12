using System;

namespace FavoDeMel.Venda.Domain.Models
{
    public class RemoverItemPedidoDTO
    {
        public Guid ClienteId { get; set; }
        public Guid ProdutoId { get; set; }

    }
}
