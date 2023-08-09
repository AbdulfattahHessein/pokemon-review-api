using PokemonReviewApp.Bases;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonsRepository : IBaseRepository<Pokemon>
    {
        decimal GetPokemonRating(int id);

    }
}