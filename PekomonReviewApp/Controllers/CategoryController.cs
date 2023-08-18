using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Bases;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Helpers;
using PokemonReviewApp.Models;

namespace CategoryReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        //Post api/categories
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        public IActionResult Add(CategoryDto categoryDto)
        {
            var category = categoryDto.MapTo<Category>();

            _unitOfWork.Categories.Insert(category);
            _unitOfWork.Complete();

            return Ok(category);
        }

        //GET api/categories
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetAll()
        {
            var categories = _unitOfWork.Categories.GetAll().MapTo<CategoryDto>();

            return Ok(categories);
        }

        //GET api/categories/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetById(int id)
        {
            try
            {
                var category = _unitOfWork.Categories.GetById(id).MapTo<CategoryDto>();
                return Ok(category);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            //var category =_categoryRepository.GetCategory(id);
            //return Ok(Category);

        }

        //GET api/categories/{id}
        [HttpGet("{categoryId}/pokemons")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonsByCategoryId(int categoryId)
        {
            try
            {
                return Ok(_unitOfWork.Categories.GetPokemonsOfCategory(categoryId).MapTo<PokemonDto>());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //GET api/categories/1
        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Update(int categoryId, [FromBody] CategoryDto? categoryDto)
        {
            try
            {
                if (!(categoryId == categoryDto?.Id && _unitOfWork.Categories.IsExist(categoryId))) return BadRequest();

                var category = categoryDto!.MapTo<Category>();

                _unitOfWork.Categories.Update(category);
                _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        //Delete api/categories/1
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int categoryId)
        {
            try
            {
                var category = _unitOfWork.Categories.GetFirstOrDefault(c => c.Id == categoryId);
                _unitOfWork.Categories.Delete(category);
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
