using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class OwnerRepository : BaseRepository<Owner>, IOwnersRepository
    {
        public OwnerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Owner> GetOwnersOfPokemon(int pokemonId)
        {
            var owners = _context.Pokemons
                            .Where(p => p.Id == pokemonId)
                            .Select(p => p.Owners).FirstOrDefault();

            return owners ?? throw new Exception($"{nameof(Pokemon)} not Found");
        }

        public IEnumerable<Pokemon> GetPokemonsOfOwner(int ownerId)
        {
            var pokemons = _context.Owners
                           .Where(o => o.Id == ownerId)
                           .Select(o => o.Pokemons).FirstOrDefault();

            return pokemons ?? throw new Exception($"{nameof(Owner)} not Found");
        }

    }
}
