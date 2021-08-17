using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Catalogo.Domain.Models.DTO;
using FavoDeMel.Domain.Core.Data;

namespace FavoDeMel.Catalogo.Domain
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<DadosPaginadoDTO<Produto>> ObterTodos(int pageSize = 10, int pageIndex = 0);
        Task<Produto> ObterPorId(Guid id);
        Task<IEnumerable<Produto>> ObterPorCategoria(int codigo);
        Task<IEnumerable<Categoria>> ObterCategorias();

        void Adicionar(Produto produto);
        void Atualizar(Produto produto);

        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
    }
}