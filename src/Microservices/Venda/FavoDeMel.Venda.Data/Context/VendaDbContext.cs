using FavoDeMel.Venda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Venda.Data.Context
{
    public class VendaDbContext : DbContext
    {
        public VendaDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<VendaLog> VendaLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VendaDbContext).Assembly);
        }
    }
}
