using Microsoft.AspNetCore.Http;
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
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


        public ReviewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Post api/reviews
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Review))]
        public IActionResult Add(int reviewerId, int pokemonId, ReviewDto reviewDto)
        {
            var review = reviewDto.MapTo<Review>();


            var reviewr = _unitOfWork.Reviewers.GetById(reviewerId);
            var pokemon = _unitOfWork.Pokemons.GetById(pokemonId);

            review.Reviewer = reviewr;
            review.Pokemon = pokemon;

            //review.Reviewer.Id = reviewerId; // pokemon must be tracked first
            //review.Pokemon.Id = pokemonId;  // pokemon must be tracked first

            _unitOfWork.Reviews.Add(review);
            _unitOfWork.Complete();

            return Ok(review.MapTo<ReviewDto>());
        }

        //GET api/reviews
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetAll()
        {
            var reviews = _unitOfWork.Reviews.GetAll().MapTo<ReviewDto>();

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
                var review = _unitOfWork.Reviews.GetById(id).MapTo<ReviewDto>();
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
                var reviews = _unitOfWork.Reviews.GetReviewsOfPokemon(pokemonId).MapTo<ReviewDto>();

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
