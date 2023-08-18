using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Bases;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Helpers;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PokemonsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            _unitOfWork.Pokemons.Insert(pokemon);

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

        //Put api/owners/1
        [HttpPut("{pokemonId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Update(int pokemonId, [FromQuery] int[] ownersIds, [FromQuery] int[] categoriesIds, [FromBody] PokemonDto? pokemonDto)
        {
            try
            {
                if (!(pokemonId == pokemonDto?.Id && _unitOfWork.Pokemons.IsExist(pokemonId))) return BadRequest(ModelState);

                var pokemon = _unitOfWork.Pokemons.GetFirstOrDefault(p => p.Id == pokemonId, new[] { nameof(Pokemon.Owners), nameof(Pokemon.Categories) });

                pokemonDto.MapTo(pokemon);

                var owners = ownersIds.Select(ownerId => _unitOfWork.Owners.GetById(ownerId)).ToList();

                var categories = categoriesIds.Select(categoryId => _unitOfWork.Categories.GetById(categoryId)).ToList();

                pokemon.Owners = owners;

                pokemon.Categories = categories;

                _unitOfWork.Pokemons.Update(pokemon);

                _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        //Delete api/owners/1
        [HttpDelete("{pokemonId}")]
        public IActionResult Delete(int pokemonId)
        {
            try
            {
                var pokemon = _unitOfWork.Pokemons.GetFirstOrDefault(c => c.Id == pokemonId, new[] { nameof(Pokemon.Reviews) });
                _unitOfWork.Pokemons.Delete(pokemon);
                _unitOfWork.Reviews.DeleteList(pokemon.Reviews!);
                _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
