using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Api.DTOs;
using PokemonReviewApp.Services.Services;

namespace PokemonReviewApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly ReviewService _service;
        private readonly IMapper _mapper;

        public ReviewController(ReviewService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_service.GetReviews());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(reviews);
            }
        }

        [HttpGet("{reviewId}")]
        public IActionResult GetReview(int reviewId)
        {
            if (!_service.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var review = _mapper.Map<ReviewDto>(_service.GetReview(reviewId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(review);
            }
        }

        [HttpGet("pokemon/{pokemonId}")]
        public IActionResult GetReviewsOfAPokemon(int pokemonId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_service.GetReviewsOfAPokemon(pokemonId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(reviews);
            }
        }
    }
}
