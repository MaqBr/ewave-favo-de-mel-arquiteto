﻿using Microsoft.AspNetCore.Mvc.Rendering;
using FavoDeMel.Presentation.MVC.ViewModels.Pagination;
using System.Collections.Generic;
using FavoDeMel.Presentation.MVC.CatalogoViewModels.ViewModels;

namespace FavoDeMel.Presentation.MVC.ViewModels.CatalogoViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<ProdutoViewModel> CatalogoItens { get; set; }
        public IEnumerable<SelectListItem> Marcas { get; set; }
        public IEnumerable<SelectListItem> Tipos { get; set; }
        public int? FiltroMarca { get; set; }
        public int? FiltroTipo { get; set; }
        public PaginacaoInfo PaginacaoInfo { get; set; }
    }
}
