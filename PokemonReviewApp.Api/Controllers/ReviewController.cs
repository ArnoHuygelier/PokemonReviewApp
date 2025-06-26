using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Api.DTOs;
using PokemonReviewApp.Core.Models;
using PokemonReviewApp.Services.Services;

namespace PokemonReviewApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly ReviewService _service;
        private readonly IMapper _mapper;
        private readonly PokemonService _pokemonService;
        private readonly ReviewerService _reviewerService;

        public ReviewController(ReviewService service, IMapper mapper, PokemonService pokemonService, ReviewerService reviewerService)
        {
            _service = service;
            _mapper = mapper;
            _pokemonService = pokemonService;
            _reviewerService = reviewerService;
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

        [HttpPost]
        public IActionResult CreateReviewer([FromQuery] int reviewerId, [FromQuery] int pokemonId, [FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewer = _mapper.Map<Review>(reviewDto);

            reviewer.Reviewer = _reviewerService.GetReviewer(reviewerId);
            reviewer.Pokemon = _pokemonService.GetPokemonById(pokemonId);

            if (!_service.CreateReview(reviewer))
            {
                ModelState.AddModelError("", "Something went wrong while saving the reviewer");
                return StatusCode(500, ModelState);
            }
            else
            {
                return Ok("Reviewer created successfully");
            }

        }
    }
}
