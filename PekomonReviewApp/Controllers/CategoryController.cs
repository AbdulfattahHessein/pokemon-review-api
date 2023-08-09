using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Helpers;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace CategoryReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        //Post api/categories
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Category))]
        public IActionResult Add(CategoryDto categoryDto)
        {
            var category = categoryDto.MapTo<Category>();

            _categoriesRepository.Add(category);
            _categoriesRepository.SaveChanges();

            return Ok(category);
        }

        //GET api/categories
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetAll()
        {
            var categories = _categoriesRepository.GetAll().MapTo<CategoryDto>();

            return Ok(categories);
        }

        //GET api/categories/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetById(int id)
        {
            try
            {
                var category = _categoriesRepository.GetById(id).MapTo<CategoryDto>();
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonsByCategoryId(int categoryId)
        {
            try
            {
                return Ok(_categoriesRepository.GetPokemonsByCategoryId(categoryId).MapTo<PokemonDto>());
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
                if (!(categoryId == categoryDto?.Id && _categoriesRepository.IsExist(categoryId))) return BadRequest();

                var category = categoryDto!.MapTo<Category>();

                _categoriesRepository.Update(category);
                _categoriesRepository.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
