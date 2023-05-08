using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Models.OrderDetailModels
{
    public class OrderDetailCreateRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
