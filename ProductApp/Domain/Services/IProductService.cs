using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Models.Queries;
using SuperMarket.API.Domain.Services.Communication;

namespace SuperMarket.API.Domain.Services
{
    public interface IProductService
    {
        Task<QueryResult<Product>> List(ProductsQuery query);
        Task<ProductResponse> Save(Product product);
        Task<ProductResponse> Update(int id, Product product);
        Task<ProductResponse> Delete(int id);
        Task<ProductResponse> GetValue(int id);
    }
}
