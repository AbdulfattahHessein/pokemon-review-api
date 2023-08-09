using PokemonReviewApp.Bases;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewersRepository : IBaseRepository<Reviewer>
    {
        IEnumerable<Review> GetReviewsOfReviwer(int reviewerId);
    }
}
