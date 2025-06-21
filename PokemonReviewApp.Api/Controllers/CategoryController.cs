using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Api.DTOs;
using PokemonReviewApp.Services.Services;

namespace PokemonReviewApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _service;
        private readonly IMapper _mapper;

        public CategoryController(CategoryService categoryService, IMapper mapper)
        {
            _service = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var pokemons = _mapper.Map<List<CategoryDto>>(_service.GetAllCategories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(pokemons);
            }
        }

        [HttpGet("{categoryId}")]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_service.DoesCategoryExist(categoryId))
            {
                return NotFound();
            }

            var category = _mapper.Map<PokemonDto>(_service.GetCategoryById(categoryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(category);
            }
        }

        [HttpGet("{categoryName}")]
        public IActionResult GetCategory(string categoryName)
        {
            if (!_service.DoesCategoryExist(categoryName))
            {
                return NotFound();
            }

            var category = _mapper.Map<PokemonDto>(_service.GetCategoryByName(categoryName));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(category);
            }
        }

        [HttpGet("pokemon/{categoryId}")]
        public IActionResult GetPokemonsByCategoryId(int categoryId)
        {
            if (!_service.DoesCategoryExist(categoryId))
            {
                return NotFound();
            }
            var pokemons = _mapper.Map<List<PokemonDto>>(_service.GetPokemonsByCategoryId(categoryId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(pokemons);
            }
        }
    }
}
