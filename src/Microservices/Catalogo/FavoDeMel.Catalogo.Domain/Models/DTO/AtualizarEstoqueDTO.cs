using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Domain.Models.DTO
{
    public class AtualizarEstoqueDTO
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
