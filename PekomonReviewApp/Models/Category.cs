using PokemonReviewApp.Bases;

namespace PokemonReviewApp.Models
{
    public class Category : IEntity<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Pokemon>? Pokemons { get; set; }

    }
}
