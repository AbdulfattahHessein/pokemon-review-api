using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Helpers;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnersRepository _ownersRepository;

        public OwnersController(IOwnersRepository ownersRepository)
        {
            _ownersRepository = ownersRepository;
        }

        //Post api/owners
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Category))]
        public IActionResult Add(OwnerDto ownerDto)
        {
            var owner = ownerDto.MapTo<Owner>();

            _ownersRepository.Add(owner);
            _ownersRepository.SaveChanges();

            return Ok(owner);
        }

        //GET api/owners
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
        public IActionResult GetAll()
        {
            var owners = _ownersRepository.GetAll().MapTo<OwnerDto>();

            return Ok(owners);
        }

        //GET api/owners/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(OwnerDto))]
        [ProducesResponseType(400)]
        public IActionResult GetById(int id)
        {
            try
            {
                var owner = _ownersRepository.GetById(id).MapTo<OwnerDto>();
                return Ok(owner);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //GET api/owners/{ownerId}/pokemons
        [HttpGet("{ownerId}/pokemons")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonsOfOwner(int ownerId)
        {
            try
            {
                return Ok(_ownersRepository.GetPokemonsOfOwner(ownerId).MapTo<PokemonDto>());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //GET api/owners/{pokemonId}/owners
        [HttpGet("{pokemonId}/owners")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnersOfPokemon(int pokemonId)
        {
            try
            {
                var owners = _ownersRepository.GetOwnersOfPokemon(pokemonId).MapTo<OwnerDto>();

                return Ok(owners);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
