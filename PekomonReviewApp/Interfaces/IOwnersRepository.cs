using PokemonReviewApp.Bases;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IOwnersRepository : IBaseRepository<Owner>
    {
        IEnumerable<Owner> GetOwnersOfPokemon(int pokemonId);
        IEnumerable<Pokemon> GetPokemonsOfOwner(int ownerId);
    }
}
