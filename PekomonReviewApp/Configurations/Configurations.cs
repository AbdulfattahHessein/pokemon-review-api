using PokemonReviewApp.Bases;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Configuration
{
    public static class ConfigurationsExtensions
    {
        internal static IServiceCollection AddRepositoriesServices(this IServiceCollection services)
        {
            services.AddTransient<IPokemonsRepository, PokemonRepository>();
            services.AddTransient<ICategoriesRepository, CategoryRepository>();
            services.AddTransient<ICountriesRepository, CountryRepository>();
            services.AddTransient<IOwnersRepository, OwnerRepository>();
            services.AddTransient<IReviewsRepository, ReviewRepository>();
            services.AddTransient<IReviewersRepository, ReviewerRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}
