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
    public class ReviewersController : ControllerBase
    {
        private readonly IReviewerRepository _reviwerRepository;

        public ReviewersController(IReviewerRepository reviewerRepository)
        {
            _reviwerRepository = reviewerRepository;
        }

        //GET api/reviewers
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        public IActionResult GetAll()
        {
            var reviewers = _reviwerRepository.GetAll().MapTo<ReviewerDto>();

            return Ok(reviewers);
        }

        //GET api/reviewers/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReviewerDto))]
        [ProducesResponseType(400)]
        public IActionResult GetById(int id)
        {
            try
            {
                var reviewer = _reviwerRepository.GetById(id).MapTo<ReviewerDto>();
                return Ok(reviewer);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //GET api/reviewers/{reviewerId}/reviews
        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfReviwer(int reviewerId)
        {
            try
            {
                var reviews = _reviwerRepository.GetReviewsOfReviwer(reviewerId).MapTo<ReviewDto>();

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
