using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Api.DTOs;
using PokemonReviewApp.Core.Models;
using PokemonReviewApp.Repository;
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
        private readonly PokemonDbContext _context;

        public PokemonController(PokemonService service, IMapper mapper, PokemonDbContext context)
        {
            _service = service;
            _mapper = mapper;
            _context = context;
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
        public IActionResult UpdatePokemon([FromQuery] int pokemonId, [FromBody] PokemonDto pokemonUpdate)
        {
            if (pokemonUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_service.DoesPokemonExist(pokemonId))
            {
                return NotFound("Pokemon not found");
            }

            var pokemonMap = _mapper.Map<Pokemon>(pokemonUpdate);

            pokemonMap.Id = pokemonId;

            if (!_service.UpdatePokemon(pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating the pokemon");
                return StatusCode(500, ModelState);
            }
            else
            {
                return Ok("Pokemon updated successfully!");
            }
        }

        // PATCH: api/Pokemon/5/owners
        [HttpPatch("{id}/owners")]
        public async Task<IActionResult> UpdatePokemonOwners(int id, [FromBody] List<int> ownerIds)
        {
            var pokemon = await _context.Pokemons
                .Include(p => p.PokemonOwners)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pokemon == null)
                return NotFound();

            // Verwijder huidige koppelingen
            pokemon.PokemonOwners.Clear();

            // Voeg nieuwe koppelingen toe
            foreach (var ownerId in ownerIds)
            {
                var owner = await _context.Owners.FindAsync(ownerId); // Zorg dat je een Owner model hebt
                if (owner != null)
                {
                    pokemon.PokemonOwners.Add(new PokemonOwner
                    {
                        PokemonId = pokemon.Id,
                        OwnerId = owner.Id
                    });
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
