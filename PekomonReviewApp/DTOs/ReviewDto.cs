namespace PokemonReviewApp.DTOs
{
    public record ReviewDto(int Id, string Title, string Text, decimal Rating);
    //public class ReviewDto : IDto
    //{
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //    public string Text { get; set; }

    //    public decimal Rating { get; set; }
    //}
}
