using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Api.DTOs;
using PokemonReviewApp.Core.Models;
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

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if(categoryCreate == null)
            {
                return BadRequest(ModelState);
            }

            var category = _service.GetCategoryByName(categoryCreate.Name);

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if(!_service.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving the category");
                return StatusCode(500, ModelState);
            }
            else
            {
                return Ok("Category created successfully!");
            }
        }

        [HttpPut]
        public IActionResult UpdateCategory([FromQuery] int categoryId, [FromBody] CategoryDto categoryUpdate)
        {
            if (categoryUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_service.DoesCategoryExist(categoryId))
            {
                return NotFound("Category not found");
            }

            var categoryMap = _mapper.Map<Category>(categoryUpdate);

            categoryMap.Id = categoryId;

            if(!_service.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating the category");
                return StatusCode(500, ModelState);
            }
            else
            {
                return Ok("Category updated successfully!");
            }
        }
    }
}
