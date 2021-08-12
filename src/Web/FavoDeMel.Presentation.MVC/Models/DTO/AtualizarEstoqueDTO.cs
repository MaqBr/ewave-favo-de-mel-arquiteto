using System;

namespace FavoDeMel.Presentation.MVC.Models.DTO
{
    public class AtualizarEstoqueDTO
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
