using PokemonReviewApp.Bases;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICategoriesRepository : IBaseRepository<Category>
    {
        IEnumerable<Pokemon> GetPokemonsByCategoryId(int categoryId);

    }
}
