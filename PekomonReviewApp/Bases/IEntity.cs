namespace PokemonReviewApp.Bases
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }

}
