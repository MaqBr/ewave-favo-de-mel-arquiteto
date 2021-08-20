using FavoDeMel.Venda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using FavoDeMel.Domain.Core.Messages;
using FavoDeMel.Domain.Core.Data;
using FavoDeMel.Domain.Core.Communication.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using FavoDeMel.Venda.Data.Repository;

namespace FavoDeMel.Venda.Data.Context
{
    public class VendaDbContext : DbContext, IUnitOfWork
    {

        public VendaDbContext(DbContextOptions<VendaDbContext> options) 
            : base(options)
        {

        }

        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<ComandaItem> ComandaItems { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        
        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            var sucesso = await base.SaveChangesAsync() > 0;

            return sucesso;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VendaDbContext).Assembly);

        }
    }
}
