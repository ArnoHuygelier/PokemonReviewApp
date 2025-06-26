using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Api.DTOs;
using PokemonReviewApp.Services.Services;

namespace PokemonReviewApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly ReviewerService _service;
        private readonly IMapper _mapper;

        public ReviewerController(ReviewerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_service.GetReviewers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(reviewers);
            }
        }

        [HttpGet("{reviewerId}")]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!_service.ReviewerExists(reviewerId))
            {
                return NotFound();
            }
            var reviewer = _mapper.Map<ReviewerDto>(_service.GetReviewer(reviewerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(reviewer);
            }
        }

        [HttpGet("{reviewerId}/reviews")]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_service.ReviewerExists(reviewerId))
            {
                return NotFound();
            }
            var reviews = _mapper.Map<List<ReviewDto>>(_service.GetReviewsByReviewer(reviewerId));
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
        public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerDto)
        {
            if (reviewerDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewer = _mapper.Map<Core.Models.Reviewer>(reviewerDto);

            if (_service.ReviewerExists(reviewer.FirstName, reviewer.LastName))
            {
                ModelState.AddModelError("", "Reviewer already exists");
                return StatusCode(400, ModelState);
            }

            if (!_service.CreateReviewer(reviewer))
            {
                ModelState.AddModelError("", "Something went wrong while saving the reviewer");
                return StatusCode(500, ModelState);
            }
            else
            {
                return Ok("Reviewer successfully created");
            }
        }
    }
}
