using System;

namespace FavoDeMel.Venda.Domain.Models
{
    public class AtualizarItemPedidoDTO
    {
        public Guid ClienteId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
