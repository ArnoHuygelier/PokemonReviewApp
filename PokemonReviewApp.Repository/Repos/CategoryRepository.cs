using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PokemonReviewApp.Core.Interfaces;
using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Repository.Repos
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PokemonDbContext _context;

        public CategoryRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category? GetCategory(int categoryId)
        {
            return _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
        }

        public Category? GetCategoryByName(string name)
        {
            return _context.Categories.Where(c => c.Name.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonsByCategoryId(int categoryId)
        {
            return _context.PokemonCategories.Where(p => p.CategoryID == categoryId)
                .Select(p => p.Pokemon).ToList();
        }

        public bool CategoriesExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }

        public bool CategoriesExists(string categoryName)
        {
            return _context.Categories.Any(c => c.Name.Trim().ToLower() == categoryName.Trim().ToLower());
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
