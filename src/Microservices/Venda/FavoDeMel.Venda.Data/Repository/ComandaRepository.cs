using FavoDeMel.Domain.Core.Data;
using FavoDeMel.Venda.Data.Context;
using FavoDeMel.Venda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Venda.Data.Repository
{
    public class ComandaRepository : IComandaRepository
    {
        private readonly VendaDbContext _context;

        public ComandaRepository(VendaDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Comanda> ObterPorId(Guid id)
        {
            return await _context.Comandas.FindAsync(id);
        }

        public async Task<IEnumerable<Comanda>> ObterListaPorStatus(ComandaStatus status)
        {
            return await _context.Comandas.AsNoTracking().Where(p => p.ComandaStatus == status).ToListAsync();
        }

        public async Task<Comanda> ObterComandaRascunhoPorMesaId(Guid mesaId)
        {
            var comanda = await _context.Comandas.FirstOrDefaultAsync(p => 

            p.MesaId == mesaId && p.ComandaStatus == ComandaStatus.Rascunho || p.ComandaStatus == ComandaStatus.Iniciado);

            if (comanda == null) return null;

            await _context.Entry(comanda)
                .Collection(i => i.ComandaItems).LoadAsync();

            if (comanda.VoucherId != null)
            {
                await _context.Entry(comanda)
                    .Reference(i => i.Voucher).LoadAsync();
            }

            if (comanda.MesaId != null)
            {
                await _context.Entry(comanda)
                    .Reference(i => i.Mesa).LoadAsync();
            }

            return comanda;
        }

        public async Task<IEnumerable<Comanda>> ObterListaPorMesaId(Guid mesaId)
        {
            return await _context.Comandas.AsNoTracking().Where(p => p.MesaId == mesaId).ToListAsync();
        }

        public void Adicionar(Comanda comanda)
        {
            _context.Comandas.Add(comanda);
        }

        public void Atualizar(Comanda comanda)
        {
            _context.Comandas.Update(comanda);
        }


        public async Task<ComandaItem> ObterItemPorId(Guid id)
        {
            return await _context.ComandaItems.FindAsync(id);
        }

        public async Task<ComandaItem> ObterItemPorComanda(Guid comandaId, Guid produtoId)
        {
            return await _context.ComandaItems.FirstOrDefaultAsync(p => p.ProdutoId == produtoId && p.ComandaId == comandaId);
        }

        public void AdicionarItem(ComandaItem comandaItem)
        {
            _context.ComandaItems.Add(comandaItem);
        }

        public void AtualizarItem(ComandaItem comandaItem)
        {
            _context.ComandaItems.Update(comandaItem);
        }

        public void AtualizarMesa(Guid mesaId, SituacaoMesa situacaoMesa)
        {
            var mesa = _context.Mesas.Find(mesaId);
            mesa.Situacao = situacaoMesa;
            _context.Mesas.Update(mesa);
        }

        public void RemoverItem(ComandaItem comandaItem)
        {
            _context.ComandaItems.Remove(comandaItem);
        }

        public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
