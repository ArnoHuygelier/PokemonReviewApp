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
        public async Task<IActionResult> GetPokemons()
        {
            var pokemons = await _service.GetPokemons();

            if(!ModelState.IsValid)
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
