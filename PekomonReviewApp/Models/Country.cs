using PokemonReviewApp.Bases;

namespace PokemonReviewApp.Models
{
    public class Country : IEntity<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Owner>? Owners { get; set; }

    }
}
