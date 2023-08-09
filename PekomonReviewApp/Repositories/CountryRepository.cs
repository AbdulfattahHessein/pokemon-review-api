using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(AppDbContext context) : base(context) { }

        public Country GetCountryByOwnerId(int ownerId)
        {
            return _context.Owners
                            .Where(c => c.Id == ownerId)
                            .Select(o => o.Country)
                            .FirstOrDefault() ?? throw new Exception("The Owner Doesn't Have A Country");
        }

        public IEnumerable<Owner> GetOwnersFromACountry(int countryId)
        {
            return _context.Owners
                            .Where(c => c.Country.Id == countryId)
                            .ToList();
        }

    }
}
