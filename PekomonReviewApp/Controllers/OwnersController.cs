﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Bases;
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
        private readonly IUnitOfWork _unitOfWork;
        public OwnersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Post api/owners
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Category))]
        public IActionResult Add(OwnerDto ownerDto)
        {
            var owner = ownerDto.MapTo<Owner>();

            _unitOfWork.Owners.Add(owner);
            _unitOfWork.Complete();

            return Ok(owner);
        }

        //GET api/owners
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
        public IActionResult GetAll()
        {
            var owners = _unitOfWork.Owners.GetAll().MapTo<OwnerDto>();

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
                var owner = _unitOfWork.Owners.GetById(id).MapTo<OwnerDto>();
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
                return Ok(_unitOfWork.Owners.GetPokemonsOfOwner(ownerId).MapTo<PokemonDto>());
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
                var owners = _unitOfWork.Owners.GetOwnersOfPokemon(pokemonId).MapTo<OwnerDto>();

                return Ok(owners);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}