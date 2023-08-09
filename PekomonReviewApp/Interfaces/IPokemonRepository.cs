using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository : IBaseRepository<Pokemon>
    {
        decimal GetPokemonRating(int id);

    }
}