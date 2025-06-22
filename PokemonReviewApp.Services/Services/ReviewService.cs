using PokemonReviewApp.Core.Interfaces;
using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Services.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Review? GetReview(int reviewId)
        {
            return _reviewRepository.GetReview(reviewId);
        }

        public ICollection<Review> GetReviews()
        {
            return _reviewRepository.GetReviews();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return _reviewRepository.GetReviewsOfAPokemon(pokeId);
        }

        public bool ReviewExists(int reviewId)
        {
            return _reviewRepository.ReviewExists(reviewId);
        }
    }
}
