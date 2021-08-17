using FavoDeMel.Presentation.MVC.CatalogoViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.ViewModels
{
    public class Catalogo
    {
        public int PageIndex { get; init; }
        public int PageSize { get; init; }
        public int Count { get; init; }
        public List<ProdutoViewModel> Data { get; init; }
    }
}
