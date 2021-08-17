﻿using System;
using System.Threading.Tasks;
using FavoDeMel.Presentation.MVC.CatalogoViewModels.ViewModels;
using FavoDeMel.Presentation.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace FavoDeMel.Presentation.MVC.Controllers.Admin
{
    public class AdminProdutosController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;

        public AdminProdutosController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        //[HttpGet]
        //[Route("admin-produtos")]
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _produtoAppService.ObterTodos());
        //}

        [Route("novo-produto")]
        public async Task<IActionResult> NovoProduto()
        {
            return View(await PopularCategorias(new ProdutoViewModel()));
        }

        [Route("novo-produto")]
        [HttpPost]
        public async Task<IActionResult> NovoProduto(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return View(await PopularCategorias(produtoViewModel));

            await _produtoAppService.AdicionarProduto(produtoViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id)
        {
            return View(await PopularCategorias(await _produtoAppService.ObterPorId(id)));
        }

        [HttpPost]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoViewModel produtoViewModel)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            produtoViewModel.QuantidadeEstoque = produto.QuantidadeEstoque;

            ModelState.Remove("QuantidadeEstoque");
            if (!ModelState.IsValid) return View(await PopularCategorias(produtoViewModel));

            await _produtoAppService.AtualizarProduto(produtoViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id)
        {
            return View("Estoque", await _produtoAppService.ObterPorId(id));
        }

        //[HttpPost]
        //[Route("produtos-atualizar-estoque")]
        //public async Task<IActionResult> AtualizarEstoque(Guid id, int quantidade)
        //{
        //    if (quantidade > 0)
        //    {
        //        await _produtoAppService.ReporEstoque(new Models.DTO.AtualizarEstoqueDTO { ProdutoId = id, Quantidade = quantidade });
        //    }
        //    else
        //    {
        //        await _produtoAppService.DebitarEstoque(new Models.DTO.AtualizarEstoqueDTO { ProdutoId = id, Quantidade = quantidade });
        //    }

        //    return View("Index", await _produtoAppService.ObterTodos());
        //}

        private async Task<ProdutoViewModel> PopularCategorias(ProdutoViewModel produto)
        {
            produto.Categorias = await _produtoAppService.ObterCategorias();
            return produto;
        }
    }
}