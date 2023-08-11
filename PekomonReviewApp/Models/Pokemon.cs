using PokemonReviewApp.Bases;

namespace PokemonReviewApp.Models
{
    public class Pokemon : IEntity<int>
    {
        public Pokemon()
        {
            Categories = new List<Category>();
            Owners = new List<Owner>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Category> Categories { get; internal set; }
        public ICollection<Owner> Owners { get; internal set; }

        public Pokemon AddOwner(Owner owner)
        {
            if (!Owners.Contains(owner))
            {
                Owners.Add(owner);
            }
            return this;
        }

        public Pokemon AddOwners(List<Owner> owners)
        {
            foreach (var owner in owners)
            {
                if (!Owners.Contains(owner))
                {
                    Owners.Add(owner);
                }
            }
            return this;
        }
        public Pokemon AddCategory(Category category)
        {
            if (!Categories.Contains(category))
            {
                Categories.Add(category);
            }
            return this;
        }
        public Pokemon AddCategoryies(List<Category> categories)
        {
            foreach (var category in categories)
            {
                if (!Categories.Contains(category))
                {
                    Categories.Add(category);
                }
            }
            return this;
        }
    }
}
