namespace PokemonReviewApp.DTOs
{
    public class ReviewerDto : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
