namespace FilmDiary.API.DTOs
{
    public class FilmDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public decimal ImdbRating { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsFavorite { get; set; }

        public List<string> Actors { get; set; } = new();

        public int TotalComments { get; set; }
        public int SpoilerComments { get; set; }
        public int NonSpoilerComments { get; set; }
    }
}