using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Models.Queries;

namespace SuperMarket.API.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<QueryResult<Product>> List(ProductsQuery query);
        Task Add(Product product);
        Task<Product> FindById(int id);
        void Update(Product product);
        void Remove(Product product);
    }
}
