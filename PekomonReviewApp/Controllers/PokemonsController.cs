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
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PokemonsController(IPokemonRepository pokemonRepository, IOwnerRepository ownerRepository, ICategoryRepository categoryRepository)
        {
            _pokemonRepository = pokemonRepository;
            _ownerRepository = ownerRepository;
            _categoryRepository = categoryRepository;
        }

        //GET api/pokemons
        [HttpPost("add")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        public IActionResult Add(int ownerId, int categoryId, [FromBody] PokemonDto pokemonDto)
        {
            var owner = _ownerRepository.GetById(ownerId);
            var category = _categoryRepository.GetById(categoryId);

            var pokemon = pokemonDto.MapTo<Pokemon>();

            pokemon.Owners = new List<Owner>() { owner };
            pokemon.Categories = new List<Category> { category };

            _pokemonRepository.Add(pokemon);
            _pokemonRepository.SaveChanges();

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
            var pokemons = _pokemonRepository.GetAll().MapTo<PokemonDto>();

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
                var pokemon = _pokemonRepository.GetById(id).MapTo<PokemonDto>();
                return Ok(pokemon);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            //var pokemon = _pokemonRepository.GetPokemon(id);
            //return Ok(pokemon);

        }

        //GET api/pokemon/{id}
        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {

            try
            {
                return Ok(_pokemonRepository.GetPokemonRating(pokeId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
