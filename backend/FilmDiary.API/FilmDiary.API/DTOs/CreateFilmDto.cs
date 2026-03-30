using System.ComponentModel.DataAnnotations;
namespace FilmDiary.API.DTOs
{
    public class CreateFilmDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Overview { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        [Range(0, 10)]
        public decimal ImdbRating { get; set; }

        public string Status { get; set; } = "Watchlist";
    }
}
