using FavoDeMel.Catalogo.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.Interfaces
{
    public interface IProdutoFinder
    {
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();
    }
}
