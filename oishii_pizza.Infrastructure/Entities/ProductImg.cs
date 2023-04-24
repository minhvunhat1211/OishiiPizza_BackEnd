using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Infrastructure.Entities
{
    public class ProductImg
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ImagePath { get; set; } 

        public string? Caption { get; set; }
    }
}
