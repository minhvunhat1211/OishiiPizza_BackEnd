using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Common.Paging
{
    public class PageResponse<T>
    {
        public int TotalRecord { get; set; }
        public List<T> Items { get; set; }
    }
}
