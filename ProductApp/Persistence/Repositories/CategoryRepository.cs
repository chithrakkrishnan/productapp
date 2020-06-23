using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Repositories;
using SuperMarket.API.Persistence.Contexts;

namespace SuperMarket.API.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository

    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> List()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task Add(Category category)
        {
            await _context.AddAsync(category);
        }

        public async Task<Category> FindById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}
