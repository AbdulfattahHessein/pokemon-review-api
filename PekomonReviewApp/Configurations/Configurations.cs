using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repositories;

namespace PokemonReviewApp.Configuration
{
    public static class ConfigurationsExtensions
    {
        internal static IServiceCollection AddRepositoriesServices(this IServiceCollection services)
        {
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewerRepository, ReviewerRepository>();

            return services;
        }
    }
}
