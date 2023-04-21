using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using oishii_pizza.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Infrastructure.Configuration
{
    public class TypeOfProductConfiguration : IEntityTypeConfiguration<TypeOfProduct>
    {
        public void Configure(EntityTypeBuilder<TypeOfProduct> builder)
        {
            builder.ToTable("TypeOfProducts");

            builder.HasKey(x => x.Id);
                           
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
