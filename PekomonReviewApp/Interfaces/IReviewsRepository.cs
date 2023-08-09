using PokemonReviewApp.Bases;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewsRepository : IBaseRepository<Review>
    {
        IEnumerable<Review> GetReviewsOfPokemon(int pokemonId);
    }
}
