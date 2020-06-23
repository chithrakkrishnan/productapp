using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMarket.API.Domain.Models;

namespace SuperMarket.API.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> List();
        Task Add(Category category);
        Task<Category> FindById(int id);
        void Update(Category category);
        void Remove(Category category);
    }
}
