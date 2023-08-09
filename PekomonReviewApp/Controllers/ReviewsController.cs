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
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        //GET api/reviews
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetAll()
        {
            var reviews = _reviewRepository.GetAll().MapTo<ReviewDto>();

            return Ok(reviews);
        }

        //GET api/reviews/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        [ProducesResponseType(400)]
        public IActionResult GetById(int id)
        {
            try
            {
                var review = _reviewRepository.GetById(id).MapTo<ReviewDto>();
                return Ok(review);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //GET api/reviews/pokemons/{pokemonId}
        [HttpGet("pokemons/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfPokemon(int pokemonId)
        {
            try
            {
                var reviews = _reviewRepository.GetReviewsOfPokemon(pokemonId).MapTo<ReviewDto>();

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
