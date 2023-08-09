using PokemonReviewApp.Bases;

namespace PokemonReviewApp.Models
{
    public class Reviewer : IEntity<int>
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
