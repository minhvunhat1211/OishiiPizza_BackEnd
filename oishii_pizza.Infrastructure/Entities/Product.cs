using oishii_pizza.Infrastructure.Common.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Infrastructure.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public int TypeOfProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public TypeOfProduct TypeOfProduct { get; set; }
        public ICollection<ProductImg>? ProductImgs { get; set; }

    }
}
