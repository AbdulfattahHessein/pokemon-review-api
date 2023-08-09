using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class ReviewerRepository : BaseRepository<Reviewer>, IReviewerRepository
    {
        public ReviewerRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Review> GetReviewsOfReviwer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }
    }
}
