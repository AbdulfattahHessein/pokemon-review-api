namespace PokemonReviewApp.DTOs
{
    public record OwnerDto(int Id, string FirstName, string LastName, string Gym) : IDto;
    //public class OwnerDto : IDto
    //{
    //    public int Id { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Gym { get; set; }
    //}
}
