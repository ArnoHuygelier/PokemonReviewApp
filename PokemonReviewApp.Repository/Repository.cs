using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PokemonDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(PokemonDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync(); // ⬅️ meteen opslaan
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(); // ⬅️ meteen opslaan
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(); // ⬅️ meteen opslaan
        }
    }
}
