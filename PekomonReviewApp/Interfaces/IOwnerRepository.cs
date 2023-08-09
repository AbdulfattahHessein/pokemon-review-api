using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IOwnerRepository : IBaseRepository<Owner>
    {
        IEnumerable<Owner> GetOwnersOfPokemon(int pokemonId);
        IEnumerable<Pokemon> GetPokemonsOfOwner(int ownerId);
    }
}
