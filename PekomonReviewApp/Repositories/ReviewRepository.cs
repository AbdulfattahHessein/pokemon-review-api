using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Review> GetReviewsOfPokemon(int pokemonId)
        {
            var reviews = _context.Reviews.Where(r => r.Pokemon.Id == pokemonId).ToList();

            return reviews;
        }
    }
}
