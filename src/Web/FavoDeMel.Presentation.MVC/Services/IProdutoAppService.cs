using FavoDeMel.Presentation.MVC.CatalogoViewModels.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Presentation.MVC.Services
{
    public interface IProdutoAppService
    {
        Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo);
        Task<ProdutoViewModel> ObterPorId(Guid id);
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();
        Task<IEnumerable<CategoriaViewModel>> ObterCategorias();

        Task AdicionarProduto(ProdutoViewModel produtoViewModel);
        Task AtualizarProduto(ProdutoViewModel produtoViewModel);

        Task DebitarEstoque(AtualizarEstoqueDTO produto);
        Task ReporEstoque(AtualizarEstoqueDTO produto);
    }
}