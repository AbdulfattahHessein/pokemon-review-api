using PokemonReviewApp.Models;

namespace PokemonReviewApp.DTOs
{
    public class ReviewDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public decimal Rating { get; set; }
    }
}
