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
        ICollection<Pokemon> GetPokemonByCategory(int categoryId);
        bool CategoriesExists(int categoryId);
    }
}
