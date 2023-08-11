using PokemonReviewApp.Bases;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICategoriesRepository : IBaseRepository<Category>
    {
        IEnumerable<Pokemon> GetPokemonsOfCategory(int categoryId);
        IEnumerable<Category> GetCategoriesOfPokemon(int categoryId);


    }
}
