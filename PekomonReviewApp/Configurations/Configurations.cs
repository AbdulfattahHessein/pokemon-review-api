using PokemonReviewApp.Bases;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Configuration
{
    public static class ConfigurationsExtensions
    {
        static ConfigurationsExtensions()
        {

        }
        internal static IServiceCollection AddRepositoriesServices(this IServiceCollection services)
        {
            services.AddScoped<IPokemonsRepository, PokemonRepository>();
            services.AddScoped<ICategoriesRepository, CategoryRepository>();
            services.AddScoped<ICountriesRepository, CountryRepository>();
            services.AddScoped<IOwnersRepository, OwnerRepository>();
            services.AddScoped<IReviewsRepository, ReviewRepository>();
            services.AddScoped<IReviewersRepository, ReviewerRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}
