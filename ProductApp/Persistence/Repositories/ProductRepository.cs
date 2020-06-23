using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Models.Queries;
using SuperMarket.API.Domain.Repositories;
using SuperMarket.API.Persistence.Contexts;

namespace SuperMarket.API.Persistence.Repositories
{
    public class ProductRepository:BaseRepository,IProductRepository
    {
        public ProductRepository(AppDbContext context)
            : base(context)
        {

        }

        public async Task<QueryResult<Product>> List(ProductsQuery query)
        {

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            IQueryable<Product> queryable = _context.Products
                .Include(p => p.Category)
                .AsNoTracking();

            if (query.CategoryId.HasValue && query.CategoryId > 0)
            {
                queryable = queryable.Where(p => p.CategoryId == query.CategoryId);
            }

            // Here I count all items present in the database for the given query, to return as part of the pagination data.
            int totalItems = await queryable.CountAsync();

            // Here a simple calculation to skip a given number of items, according to the current page and amount of items per page,
            // and return them only the amount of desired items. The methods "Skip" and "Take" do the trick here.
            List<Product> products = await queryable.Skip((query.Page - 1) * query.ItemsPerPage)
                .Take(query.ItemsPerPage)
                .ToListAsync();

            //Include(p => p.Category).  to include as many entities as necessary when querying data.
            //EF Core is going to translate it to a join when performing the select.
            // Finally I return a query result, containing all items and the amount of items in the database (necessary for client-side calculations ).
            return new QueryResult<Product>
            {
                Items = products,
                TotalItems = totalItems,
            };
        }
        public async Task Add(Product product)
        {
            await _context.AddAsync(product);
        }
        public async Task<Product> FindById(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}
