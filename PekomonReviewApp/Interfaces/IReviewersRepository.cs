using PokemonReviewApp.Bases;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewersRepository : IBaseRepository<Reviewer>
    {
        IEnumerable<Review> GetReviewsOfReviwer(int reviewerId);
    }
}
