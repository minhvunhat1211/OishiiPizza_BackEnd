using oishii_pizza.Domain.Common.BaseModel;
using oishii_pizza.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Models.ProductModels
{
    public class ProductDTO : BaseModel
    {
        public int Id { get; set; }
        public int TypeOfProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public ICollection<ProductImg>? ProductImgs { get; set; }
    }
}
