using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class PokemonRepository : BaseRepository<Pokemon>, IPokemonRepository
    {
        private readonly DbSet<Pokemon> _pokemons;
        public PokemonRepository(AppDbContext context) : base(context)
        {
            _pokemons = _context.Pokemons;
        }

        public decimal GetPokemonRating(int id)
        {
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var reviews = _pokemons
                .AsNoTracking()
                .Include(p => p.Reviews)
                .FirstOrDefault(p => p.Id == id)?
                .Reviews ?? throw new Exception("Pokemon Not Found");

            return (reviews?.Sum(r => r.Rating) / reviews?.Count) ?? 0;
        }

    }
}
