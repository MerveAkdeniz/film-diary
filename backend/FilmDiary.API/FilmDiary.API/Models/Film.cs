namespace FilmDiary.API.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public decimal ImdbRating { get; set; }
        public string Status { get; set; } = string.Empty;
        // Watched, Watching, Watchlist
        public bool IsFavorite { get; set; } = false;
        public List<FilmActor> FilmActors { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}
