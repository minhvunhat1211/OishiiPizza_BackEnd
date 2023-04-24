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
    public class ProductImgConfiguration : IEntityTypeConfiguration<ProductImg>
    {
        public void Configure(EntityTypeBuilder<ProductImg> builder)
        {
            builder.ToTable("ProductImgs");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ImagePath);
            builder.Property(x => x.Caption);
        }
    }
}
