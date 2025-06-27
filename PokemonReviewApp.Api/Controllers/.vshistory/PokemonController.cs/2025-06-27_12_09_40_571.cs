using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Api.DTOs;
using PokemonReviewApp.Core.Models;
using PokemonReviewApp.Services.Services;
using System.Threading.Tasks;

namespace PokemonReviewApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly PokemonService _service;

        private readonly IMapper _mapper;

        public PokemonController(PokemonService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_service.GetAllPokemons());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(pokemons);
            }
        }

        [HttpGet("{pokemonId}")]
        public IActionResult GetPokemon(int pokemonId)
        {
            if (!_service.DoesPokemonExist(pokemonId))
            {
                return NotFound();
            }

            var pokemon = _mapper.Map<PokemonDto>(_service.GetPokemonById(pokemonId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(pokemon);
            }
        }

        [HttpGet("rating/{pokemonId}")]
        public IActionResult GetPokemonRating(int pokemonId)
        {
            if (!_service.DoesPokemonExist(pokemonId))
            {
                return NotFound();
            }
            var rating = _service.GetPokemonRating(pokemonId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(rating);
            }
        }

        [HttpPost]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemonDto)
        {
            if (pokemonDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pokemon = _mapper.Map<Pokemon>(pokemonDto);

            var pokemonExists = _service.GetPokemonByName(pokemon.Name);
            if (pokemonExists != null)
            {
                ModelState.AddModelError("", "Pokemon already exists with this name");
                return StatusCode(400, ModelState);
            }

            if (!_service.CreatePokemon(ownerId, categoryId, pokemon))
            {
                ModelState.AddModelError("", "Something went wrong while saving the Pokemon");
                return StatusCode(500, ModelState);
            }

            else
            {
                return Ok("Pokemon created successfully");
            }
        }

        [HttpPut]
        public IActionResult UpdatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] OwnerDto ownerUpdate)
        {
            if (ownerUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_service.OwnerExists(ownerId))
            {
                return NotFound("Owner not found");
            }

            var ownerMap = _mapper.Map<Owner>(ownerUpdate);

            ownerMap.Id = ownerId;

            if (!_service.UpdateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating the owner");
                return StatusCode(500, ModelState);
            }
            else
            {
                return Ok("Owner updated successfully!");
            }
        }
    }
}
