using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewerRepository : IBaseRepository<Reviewer>
    {
        IEnumerable<Review> GetReviewsOfReviwer(int reviewerId);
    }
}
