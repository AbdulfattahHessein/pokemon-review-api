using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        IEnumerable<Pokemon> GetPokemonsByCategoryId(int categoryId);

    }
}
