using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Services.Services;
using System.Threading.Tasks;

namespace PokemonReviewApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly PokemonService _service;

        public PokemonController(PokemonService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetPokemons()
        {
            var pokemons = _service.GetAllPokemons();

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
            var pokemon = _service.GetPokemonById(pokemonId);
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
    }
}
