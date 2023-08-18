using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Bases;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Helpers;
using PokemonReviewApp.Models;

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

            _unitOfWork.Reviewers.Insert(reviewer);
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

        //Put api/reviewers/1
        [HttpPut("{reviewerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Update(int reviewerId, [FromBody] ReviewerDto? reviewerDto)
        {
            try
            {
                if (!(reviewerId == reviewerDto?.Id && _unitOfWork.Reviewers.IsExist(reviewerId)))
                    return BadRequest();

                var owner = reviewerDto!.MapTo<Reviewer>();

                _unitOfWork.Reviewers.Update(owner);

                _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        //Delete api/reviewers/1
        [HttpDelete("{reviewerId}")]
        public IActionResult Delete(int reviewerId)
        {
            try
            {
                var reviewer = _unitOfWork.Reviewers.GetFirstOrDefault(c => c.Id == reviewerId, new[] { nameof(Reviewer.Reviews) });
                _unitOfWork.Reviewers.Delete(reviewer);
                _unitOfWork.Reviews.DeleteList(reviewer.Reviews!);
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
