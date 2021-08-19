using FavoDeMel.Domain.Core.Data;
using FavoDeMel.Venda.Data.Context;
using FavoDeMel.Venda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Venda.Data.Repository
{
    public class MesaRepository : IMesaRepository
    {
        private readonly VendaDbContext _context;

        public MesaRepository(VendaDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Mesa> ObterPorId(Guid id)
        {
            return await _context.Mesas.FindAsync(id);
        }

        public void Adicionar(Mesa mesa)
        {
            _context.Mesas.Add(mesa);
        }

        public void Atualizar(Mesa mesa)
        {
            _context.Mesas.Update(mesa);
        }

        public void Remover(Mesa mesa)
        {
            _context.Mesas.Remove(mesa);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<Mesa>> ObterTodos()
        {
            return await _context.Mesas.ToListAsync();
        }
    }
}
