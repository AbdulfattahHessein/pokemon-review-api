using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        IEnumerable<Review> GetReviewsOfPokemon(int pokemonId);
    }
}
