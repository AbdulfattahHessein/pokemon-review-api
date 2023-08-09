using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }

    }
}
