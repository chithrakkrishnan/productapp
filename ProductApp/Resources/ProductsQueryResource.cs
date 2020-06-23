using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarket.API.Resources
{
    public class ProductsQueryResource:QueryResource
    { public int? CategoryId { get; set; }
    }
}
