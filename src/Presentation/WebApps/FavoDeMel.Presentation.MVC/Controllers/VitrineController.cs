using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FavoDeMel.Presentation.MVC.Services;
using FavoDeMel.Presentation.MVC.ViewModels.CatalogoViewModels;
using FavoDeMel.Presentation.MVC.ViewModels.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FavoDeMel.Presentation.MVC.Controllers
{
    public class VitrineController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;

        public VitrineController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index(int? filtroMarca, int? filtroTipo, int? pagina)
        {
            var itemsPage = 9;
            var catalogo = await _produtoAppService.ObterTodos(pagina ?? 0, itemsPage, filtroMarca, filtroTipo);

            var marcas = new List<SelectListItem>
            {
                new SelectListItem() { 
                    Value = null, Text = "Todos", Selected = true                    
                }
            };

            var tipos = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "Todos", Selected = true }
            };

            var vm = new IndexViewModel()
            {
                CatalogoItens = catalogo.Data,
                Marcas = marcas, 
                Tipos = tipos,
                FiltroMarca = filtroMarca ?? 0,
                FiltroTipo = filtroTipo ?? 0,
                PaginacaoInfo = new PaginacaoInfo()
                {
                    ActualPage = pagina ?? 0,
                    ItemsPerPage = catalogo.Data.Count,
                    TotalItems = catalogo.Count,
                    TotalPages = (int)Math.Ceiling(((decimal)catalogo.Count / itemsPage))
                }
            };

            vm.PaginacaoInfo.Next = (vm.PaginacaoInfo.ActualPage == vm.PaginacaoInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginacaoInfo.Previous = (vm.PaginacaoInfo.ActualPage == 0) ? "is-disabled" : "";

            return View(vm);
        }


        [HttpGet]
        [Route("ProdutoDetalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            return View(await _produtoAppService.ObterPorId(id));
        }
    }
}