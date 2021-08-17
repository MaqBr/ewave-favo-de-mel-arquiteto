using System;

namespace FavoDeMel.Venda.Domain.Models
{
    public class RemoverItemComandaDTO
    {
        public Guid ClienteId { get; set; }
        public Guid ProdutoId { get; set; }

    }
}
