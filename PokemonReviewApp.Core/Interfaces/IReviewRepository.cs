using PokemonReviewApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Core.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review? GetReview(int reviewId);
        ICollection<Review> GetReviewsOfAPokemon(int pokeId);
        bool ReviewExists(int reviewId);
    }
}
