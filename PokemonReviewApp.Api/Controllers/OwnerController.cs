using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Api.DTOs;
using PokemonReviewApp.Services.Services;

namespace PokemonReviewApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly OwnerService _service;
        private readonly IMapper _mapper;

        public OwnerController(OwnerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_service.GetOwners());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(owners);
            }
        }

        [HttpGet("{ownerId}")]
        public IActionResult GetOwner(int ownerId)
        {
            if (!_service.OwnerExists(ownerId))
            {
                return NotFound();
            }
            var owner = _mapper.Map<OwnerDto>(_service.GetOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(owner);
            }
        }

        [HttpGet("pokemon/{pokemonId}")]
        public IActionResult GetOwnerOfAPokemon(int pokemonId)
        {
            var owners = _mapper.Map<List<OwnerDto>>(_service.GetOwnerOfAPokemon(pokemonId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(owners);
            }
        }

        [HttpGet("pokemon/{ownerId}")]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_service.GetPokemonByOwner(ownerId));
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
