using FavoDeMel.Catalogo.Data.EF.Context;
using FavoDeMel.Catalogo.Domain;
using FavoDeMel.Catalogo.Domain.Models.DTO;
using FavoDeMel.Domain.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Data.EF.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoDbContext _context;

        public ProdutoRepository(CatalogoDbContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<DadosPaginadoDTO<Produto>> ObterTodos(int pageSize = 10, int pageIndex = 0)
        {
            var total = await _context.Produtos
                .LongCountAsync();

            var itemsPorPagina = await _context.Produtos
                    .OrderBy(c => c.Nome)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize)
                    .ToListAsync();

            var model = new DadosPaginadoDTO<Produto>(pageIndex, pageSize, total, itemsPorPagina);

            return model;
        }


        public async Task<Produto> ObterPorId(Guid id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<IEnumerable<Produto>> ObterPorCategoria(int codigo)
        {
            return await _context.Produtos.AsNoTracking()
                .Include(p => p.Categoria)
                .Where(c => c.Categoria.Codigo == codigo).ToListAsync();
        }

        public async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await _context.Categorias.AsNoTracking().ToListAsync();
        }

        public void Adicionar(Produto produto)
        {
            _context.Produtos.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
        }

        public void Adicionar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
        }

        public void Atualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
