using FavoDeMel.Presentation.MVC.CatalogoViewModels.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using FavoDeMel.Presentation.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.Services
{
    public interface IProdutoAppService
    {
        Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo);
        Task<ProdutoViewModel> ObterPorId(Guid id);
        Task<Catalogo> ObterTodos(int pagina, int itensPagina, int? filtroMarca, int? filtroTipo);
        Task<IEnumerable<CategoriaViewModel>> ObterCategorias();

        Task AdicionarProduto(ProdutoViewModel produtoViewModel);
        Task AtualizarProduto(ProdutoViewModel produtoViewModel);

        Task DebitarEstoque(AtualizarEstoqueDTO produto);
        Task ReporEstoque(AtualizarEstoqueDTO produto);
    }
}