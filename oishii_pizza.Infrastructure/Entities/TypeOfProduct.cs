using oishii_pizza.Infrastructure.Common.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Infrastructure.Entities
{
    public class TypeOfProduct : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
