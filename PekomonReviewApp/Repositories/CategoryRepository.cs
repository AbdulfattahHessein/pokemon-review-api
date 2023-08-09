using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Pokemon> GetPokemonsByCategoryId(int categoryId)
        {
            //return _context
            //       .Categories
            //       .Where(c => c.Id == categoryId)
            //       .Include(p => p.Pokemons).FirstOrDefault()?
            //       .Pokemons ?? throw new Exception("There is no pokemons for this category");

            return _context
                  .Categories
                  .Where(c => c.Id == categoryId)
                  .Select(c => c.Pokemons)
                  .FirstOrDefault() ?? throw new Exception("There is no pokemons for this category");
        }
    }
}
