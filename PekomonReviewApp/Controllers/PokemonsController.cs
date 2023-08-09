using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Bases;
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
        private readonly IUnitOfWork _unitOfWork;

        public PokemonsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET api/pokemons
        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        public IActionResult Add(int ownerId, int categoryId, [FromBody] PokemonDto pokemonDto)
        {
            var owner = _unitOfWork.Owners.GetById(ownerId);
            var category = _unitOfWork.Categories.GetById(categoryId);

            var pokemon = pokemonDto.MapTo<Pokemon>();

            pokemon.AddOwner(owner).AddCategory(category);

            _unitOfWork.Pokemons.Add(pokemon);

            _unitOfWork.Complete();

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
            var pokemons = _unitOfWork.Pokemons.GetAll().MapTo<PokemonDto>();

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
                var pokemon = _unitOfWork.Pokemons.GetById(id).MapTo<PokemonDto>();
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
                return Ok(_unitOfWork.Pokemons.GetPokemonRating(pokeId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
