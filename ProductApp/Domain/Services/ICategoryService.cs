using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Services.Communication;
using SuperMarket.API.Resources;

namespace SuperMarket.API.Domain.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> List();

        Task<CategoryResponse> Save(Category category);
        Task<CategoryResponse> Update(int id, Category category);
        Task<CategoryResponse> Delete(int id);
    }
}
