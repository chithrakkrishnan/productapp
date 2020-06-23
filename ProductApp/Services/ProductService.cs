using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Models.Queries;
using SuperMarket.API.Domain.Repositories;
using SuperMarket.API.Domain.Services;
using SuperMarket.API.Domain.Services.Communication;
using SuperMarket.API.Infrastructure;

namespace SuperMarket.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork,IMemoryCache cache)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<QueryResult<Product>> List(ProductsQuery query)
        {
            string cacheKey = GetCacheKeyForProductsQuery(query);
            var products = await _cache.GetOrCreateAsync(cacheKey, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _productRepository.List(query);
            });
            return products;
        }

        public async Task<ProductResponse> Save(Product product)
        {
            var existingCategory = await _categoryRepository.FindById(product.CategoryId);
            if (existingCategory == null)
                return new ProductResponse("Invalid category.");

            await _productRepository.Add(product);
            await _unitOfWork.Complete();

            return new ProductResponse(product);
        }

        public async Task<ProductResponse> Update(int id, Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductResponse> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductResponse> GetValue(int id)
        {
            var existingProduct = await _productRepository.FindById(id);
            return new ProductResponse(existingProduct);
        }

        private string GetCacheKeyForProductsQuery(ProductsQuery query)
        {
            string key = CacheKeys.ProductsList.ToString();

            if (query.CategoryId.HasValue && query.CategoryId > 0)
            {
                key = string.Concat(key, "_", query.CategoryId.Value);
            }

            key = string.Concat(key, "_", query.Page, "_", query.ItemsPerPage);
            return key;
        }
    }
}

