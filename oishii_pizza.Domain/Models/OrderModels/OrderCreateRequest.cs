﻿using oishii_pizza.Domain.Models.OrderDetailModels;
using oishii_pizza.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Models.OrderModels
{
    public class OrderCreateRequest
    {
        public string Title { get; set; }
        public string? Note { get; set; }
        public string NameCustomer { get; set; }
        public string PhoneNumberCustomer { get; set; }
        public string AddressCustomer { get; set; }
        public float TotalPrice { get; set; }
        public ICollection<OrderDetailCreateRequest> OrderDetails { get; set; }
    }
}
