using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountryRepository : IBaseRepository<Country>
    {

        Country GetCountryByOwnerId(int ownerId);
        IEnumerable<Owner> GetOwnersFromACountry(int countryId);


    }
}
