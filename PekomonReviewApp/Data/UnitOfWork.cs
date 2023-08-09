using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Bases;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(ICategoriesRepository categories, IPokemonsRepository pokemons, ICountriesRepository countries, IOwnersRepository owners, IReviewersRepository reviewers, IReviewsRepository reviews, AppDbContext context = null)
        {
            _context = context;

            Categories = categories;
            Pokemons = pokemons;
            Countries = countries;
            Owners = owners;
            Reviewers = reviewers;
            Reviews = reviews;
        }

        public ICategoriesRepository Categories { get; }
        public IPokemonsRepository Pokemons { get; }
        public ICountriesRepository Countries { get; }
        public IOwnersRepository Owners { get; }
        public IReviewersRepository Reviewers { get; }
        public IReviewsRepository Reviews { get; }
        public void Complete()
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
