using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Api.DTOs;
using PokemonReviewApp.Core.Models;
using PokemonReviewApp.Services.Services;

namespace PokemonReviewApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly OwnerService _service;
        private readonly CountryService _countryService;
        private readonly IMapper _mapper;

        public OwnerController(OwnerService service, IMapper mapper, CountryService countryService)
        {
            _service = service;
            _countryService = countryService;
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

        [HttpPost]
        public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto owner)
        {
            if (owner == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ownerMap = _mapper.Map<Core.Models.Owner>(owner);

            ownerMap.Country = _countryService.GetCountry(countryId);

            if (_service.OwnerExists(ownerMap.FirstName, ownerMap.LastName))
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(400, ModelState);
            }

            if (!_service.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving the owner");
                return StatusCode(500, ModelState);
            }
            else
            {
                return Ok("Owner created successfully");
            }
        }

        [HttpPut]
        public IActionResult UpdateOwner([FromQuery] int ownerId, [FromBody] OwnerDto ownerUpdate)
        {
            if (ownerUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var country = _service.GetOwner(ownerId);

            if (country == null)
            {
                return NotFound("Country not found");
            }

            var ownerMap = _mapper.Map<Owner>(ownerUpdate);

            ownerMap.Id = ownerId;

            if (!_service.UpdateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating the country");
                return StatusCode(500, ModelState);
            }
            else
            {
                return Ok("Country updated successfully!");
            }
        }
    }
}
