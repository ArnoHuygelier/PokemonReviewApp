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

        public bool ReviewerExists(string firstName, string lastName)
        {
            return _context.Reviewers
                .Any(r => r.FirstName.ToLower().Trim() == firstName.ToLower().Trim() &&
                          r.LastName.ToLower().Trim() == lastName.ToLower().Trim());
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Reviewers.Add(reviewer);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
