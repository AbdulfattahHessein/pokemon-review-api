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
    public class ReviewersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        //Post api/reviewers
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ReviewerDto))]
        public IActionResult Add(ReviewerDto reviewerDto)
        {
            var reviewer = reviewerDto.MapTo<Reviewer>();

            _unitOfWork.Reviewers.Add(reviewer);
            _unitOfWork.Complete();

            return Ok(reviewer.MapTo<ReviewerDto>());
        }

        //GET api/reviewers
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        public IActionResult GetAll()
        {
            var reviewers = _unitOfWork.Reviewers.GetAll().MapTo<ReviewerDto>();

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
                var reviewer = _unitOfWork.Reviewers.GetById(id).MapTo<ReviewerDto>();
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
                var reviews = _unitOfWork.Reviewers.GetReviewsOfReviwer(reviewerId).MapTo<ReviewDto>();

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
