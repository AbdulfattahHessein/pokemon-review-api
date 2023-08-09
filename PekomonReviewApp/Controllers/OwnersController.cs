using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Helpers;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnersController(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        //GET api/owners
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
        public IActionResult GetAll()
        {
            var owners = _ownerRepository.GetAll().MapTo<OwnerDto>();

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
                var owner = _ownerRepository.GetById(id).MapTo<OwnerDto>();
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
                return Ok(_ownerRepository.GetPokemonsOfOwner(ownerId).MapTo<PokemonDto>());
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
                var owners = _ownerRepository.GetOwnersOfPokemon(pokemonId).MapTo<OwnerDto>();

                return Ok(owners);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
