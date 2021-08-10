using FavoDeMel.Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Data.EF.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Domain.Models.Catalogo>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Catalogo> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
