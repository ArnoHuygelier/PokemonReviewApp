using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Core.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category? GetCategory(int categoryId);
        Category? GetCategoryByName(string name);
        ICollection<Pokemon> GetPokemonsByCategoryId(int categoryId);
        bool CategoriesExists(int categoryName);
        bool CategoriesExists(string categoryName);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool Save();
    }
}
