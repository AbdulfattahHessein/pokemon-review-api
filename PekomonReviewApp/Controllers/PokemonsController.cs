using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Helpers;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokemonsRepository _pokemonsRepository;
        private readonly IOwnersRepository _ownersRepository;
        private readonly ICategoriesRepository _categoriesRepository;

        public PokemonsController(IPokemonsRepository pokemonsRepository, IOwnersRepository ownersRepository, ICategoriesRepository categoriesRepository)
        {
            _pokemonsRepository = pokemonsRepository;
            _ownersRepository = ownersRepository;
            _categoriesRepository = categoriesRepository;
        }

        //GET api/pokemons
        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        public IActionResult Add(int ownerId, int categoryId, [FromBody] PokemonDto pokemonDto)
        {
            var owner = _ownersRepository.GetById(ownerId);
            var category = _categoriesRepository.GetById(categoryId);

            var pokemon = pokemonDto.MapTo<Pokemon>();

            pokemon.AddOwner(owner).AddCategory(category);

            _pokemonsRepository.Add(pokemon);

            _pokemonsRepository.SaveChanges();

            return Ok(new
            {
                pokemon = pokemon.MapTo<PokemonDto>(),
                ownerId,
                owner.FirstName,
                categoryId,
                category.Name
            });
        }

        //GET api/pokemons
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetAll()
        {
            var pokemons = _pokemonsRepository.GetAll().MapTo<PokemonDto>();

            return Ok(pokemons);
        }

        //GET api/pokemon/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetById(int id)
        {
            try
            {
                var pokemon = _pokemonsRepository.GetById(id).MapTo<PokemonDto>();
                return Ok(pokemon);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //GET api/pokemon/{id}
        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {

            try
            {
                return Ok(_pokemonsRepository.GetPokemonRating(pokeId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
