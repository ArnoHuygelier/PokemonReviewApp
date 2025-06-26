using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Api.DTOs;
using PokemonReviewApp.Core.Models;
using PokemonReviewApp.Services.Services;

namespace PokemonReviewApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly CountryService _service;
        private readonly IMapper _mapper;

        public CountryController(CountryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_service.GetCountries());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(countries);
            }
        }

        [HttpGet("{countryId}")]
        public IActionResult GetCountry(int countryId)
        {
            if (!_service.CountryExists(countryId))
            {
                return NotFound();
            }
            var country = _mapper.Map<CountryDto>(_service.GetCountry(countryId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(country);
            }
        }

        [HttpGet("owner/{ownerId}")]
        public IActionResult GetCountryByOwner(int ownerId)
        {
            var country = _mapper.Map<CountryDto>(_service.GetCountryByOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(country);
            }
        }

        [HttpGet("owners/{countryId}")]
        public IActionResult GetOwnersFromCountry(int countryId)
        {
            if (!_service.CountryExists(countryId))
            {
                return NotFound();
            }
            var owners = _mapper.Map<List<OwnerDto>>(_service.GetOwnersFromCountry(countryId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(owners);
            }
        }

        [HttpPost]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
            {
                return BadRequest(ModelState);
            }

            var country = _service.GetCountry(countryCreate.Name);

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(countryCreate);

            if (!_service.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving the country");
                return StatusCode(500, ModelState);
            }
            else
            {
                return Ok("Country successfully created");
            }
        }

        [HttpPut]
        public IActionResult UpdateCountry([FromQuery] int countryId, [FromBody] CountryDto countryUpdate)
        {
            if (countryUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var country = _service.GetCountry(countryId);

            if (country == null)
            {
                return NotFound("Category not found");
            }

            var categoryMap = _mapper.Map<Category>(countryUpdate);

            categoryMap.Id = countryId;

            if (!_service.UpdateCategory(categoryMap))
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
