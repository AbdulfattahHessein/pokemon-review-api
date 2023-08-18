using PokemonReviewApp.Bases;

namespace PokemonReviewApp.Models
{
    public class Review : IEntity<int>
    {

        //public Review()
        //{
        //    Reviewer = new Reviewer();
        //    Pokemon = new Pokemon();
        //}
        public Review()
        {

        }

        public Review(int id, string? title, string? text, Reviewer reviewer, Pokemon pokemon, decimal rating)
        {
            Id = id;
            Title = title;
            Text = text;
            Reviewer = reviewer;
            Pokemon = pokemon;
            Rating = rating;
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public Reviewer? Reviewer { get; set; }
        public Pokemon? Pokemon { get; set; }
        public decimal Rating { get; set; }
    }
}
