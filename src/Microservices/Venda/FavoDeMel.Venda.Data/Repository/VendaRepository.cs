using FavoDeMel.Venda.Data.Context;
using FavoDeMel.Venda.Domain.Interfaces;
using FavoDeMel.Venda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Venda.Data.Repository
{
    public class VendaRepository : IVendaRepository
    {
        private VendaDbContext _ctx;

        public VendaRepository(VendaDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Add(VendaLog VendaLog)
        {
            _ctx.VendaLogs.Add(VendaLog);
            _ctx.SaveChanges();
        }

        public IEnumerable<VendaLog> GetVendaLogs()
        {
            return _ctx.VendaLogs;
        }
    }
}
