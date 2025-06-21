using PokemonReviewApp.Core.Interfaces;
using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Services.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ICollection<Category> GetAllCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public Category? GetCategoryById(int id)
        {
            return _categoryRepository.GetCategory(id);
        }

        public Category? GetCategoryByName(string name)
        {
            return _categoryRepository.GetCategoryByName(name);
        }

        public ICollection<Pokemon> GetPokemonsByCategory(int categoryId)
        {
            return _categoryRepository.GetPokemonByCategory(categoryId);
        }

        public bool DoesCategoryExist(int categoryId)
        {
            return _categoryRepository.CategoriesExists(categoryId);
        }
    }
}
