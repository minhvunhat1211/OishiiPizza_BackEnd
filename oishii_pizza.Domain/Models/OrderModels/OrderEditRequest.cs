using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Models.OrderModels
{
    public class OrderEditRequest
    {
        public string Title { get; set; }
        public string? Note { get; set; }
        public string NameCustomer { get; set; }
        public string PhoneNumberCustomer { get; set; }
        public string AddressCustomer { get; set; }
    }
}
