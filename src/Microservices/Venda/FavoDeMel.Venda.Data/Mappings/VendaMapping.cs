using FavoDeMel.Venda.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroRabbit.Banking.Data.Mappings
{
    public class VendaMapping : IEntityTypeConfiguration<VendaLog>
    {
        public void Configure(EntityTypeBuilder<VendaLog> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
