using PokemonReviewApp.Core.Interfaces;
using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Repository.Repos
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly PokemonDbContext _context;

        public ReviewerRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public Reviewer? GetReviewer(int reviewerId)
        {
            return _context.Reviewers
                .Where(r => r.Id == reviewerId)
                .FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews
                .Where(r => r.Reviewer.Id == reviewerId)
                .ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers
                .Any(r => r.Id == reviewerId);
        }
    }
}
