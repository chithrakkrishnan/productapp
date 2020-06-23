using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Repositories;
using SuperMarket.API.Domain.Services;
using SuperMarket.API.Domain.Services.Communication;

namespace SuperMarket.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> List()
        {
            return await _categoryRepository.List();
        }

        public async Task<CategoryResponse> Save(Category category)
        {
            try
            {
                await _categoryRepository.Add(category);
                await _unitOfWork.Complete();
                return new CategoryResponse(category);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CategoryResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> Update(int id, Category category)
        {
            var existingCategory = await _categoryRepository.FindById(id);
            if (existingCategory == null)
            {
                return new CategoryResponse("Category not found");
            }

            try
            {
                _categoryRepository.Update(category);
                await _unitOfWork.Complete();
                return new CategoryResponse(category);
            }
            catch (Exception ex)
            {
                return new CategoryResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }
        
        public async Task<CategoryResponse> Delete(int id)
        {
            var existingCategory = await _categoryRepository.FindById(id);

            if (existingCategory == null)
                return new CategoryResponse("Category not found.");

            try
            {
                _categoryRepository.Remove(existingCategory);
                await _unitOfWork.Complete();

                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CategoryResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

    }
}
    
