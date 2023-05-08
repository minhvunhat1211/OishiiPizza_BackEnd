using oishii_pizza.Infrastructure.Common.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Infrastructure.Entities
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Note { get; set; }
        public string NameCustomer { get; set; }
        public string PhoneNumberCustomer { get; set; }
        public string AddressCustomer { get; set; }
        public float TotalPrice { get; set; }
    }
}
