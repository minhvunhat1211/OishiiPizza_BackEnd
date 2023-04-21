
using oishii_pizza.Domain.Common.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Models.TypeOfProductModels
{
    public class TypeOfProductDTO : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
