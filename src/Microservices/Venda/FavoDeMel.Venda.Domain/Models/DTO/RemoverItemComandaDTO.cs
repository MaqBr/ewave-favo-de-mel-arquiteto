using System;

namespace FavoDeMel.Venda.Domain.Models
{
    public class RemoverItemComandaDTO
    {
        public Guid MesaId { get; set; }
        public Guid ProdutoId { get; set; }

    }
}
