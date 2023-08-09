using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Bases;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Helpers;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Post api/countries
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult Add(CountryDto categoryDto)
        {
            var country = categoryDto.MapTo<Country>();

            _unitOfWork.Countries.Add(country);
            _unitOfWork.Complete();

            return Ok(country.MapTo<CountryDto>());
        }

        //GET api/countries
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetAll()
        {
            var countries = _unitOfWork.Countries.GetAll().MapTo<CountryDto>();

            return Ok(countries);
        }

        //GET api/countries/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetById(int id)
        {
            try
            {
                var country = _unitOfWork.Countries.GetById(id).MapTo<CountryDto>();
                return Ok(country);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //GET api/countries/{ownerId}/country
        [HttpGet("{ownerId}/country")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryByOwnerId(int ownerId)
        {
            try
            {
                return Ok(_unitOfWork.Countries.GetCountryByOwnerId(ownerId).MapTo<CountryDto>());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //GET api/{countryId}/owners
        [HttpGet("{countryId}/owners")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnersFromACountry(int countryId)
        {
            try
            {
                var owners = _unitOfWork.Countries.GetOwnersFromACountry(countryId).MapTo<OwnerDto>();
                var country = _unitOfWork.Countries.GetById(countryId).MapTo<CountryDto>();

                return Ok(new
                {
                    country,
                    owners
                });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
