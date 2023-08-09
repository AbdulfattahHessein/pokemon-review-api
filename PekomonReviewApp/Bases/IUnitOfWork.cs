using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Bases
{

    public interface IUnitOfWork : IDisposable
    {
        ICategoriesRepository Categories { get; }
        IPokemonsRepository Pokemons { get; }
        ICountriesRepository Countries { get; }
        IOwnersRepository Owners { get; }
        IReviewersRepository Reviewers { get; }
        IReviewsRepository Reviews { get; }
        void Complete();
    }
}