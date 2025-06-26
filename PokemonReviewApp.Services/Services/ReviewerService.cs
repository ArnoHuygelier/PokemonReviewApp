using PokemonReviewApp.Core.Interfaces;
using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Services.Services
{
    public class ReviewerService
    {
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewerService(IReviewerRepository reviewerRepository)
        {
            _reviewerRepository = reviewerRepository;
        }

        public Reviewer? GetReviewer(int reviewerId)
        {
            return _reviewerRepository.GetReviewer(reviewerId);
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _reviewerRepository.GetReviewers();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _reviewerRepository.GetReviewsByReviewer(reviewerId);
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _reviewerRepository.ReviewerExists(reviewerId);
        }

        public bool ReviewerExists(string firstName, string lastName)
        {
            return _reviewerRepository.ReviewerExists(firstName, lastName);
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            if (reviewer == null)
            {
                return false;
            }
            return _reviewerRepository.CreateReviewer(reviewer);
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            if (reviewer == null)
            {
                return false;
            }
            return _reviewerRepository.UpdateReviewer(reviewer);
        }
    }
}
