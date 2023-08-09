using PokemonReviewApp.Bases;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountriesRepository : IBaseRepository<Country>
    {

        Country GetCountryByOwnerId(int ownerId);
        IEnumerable<Owner> GetOwnersFromACountry(int countryId);


    }
}
