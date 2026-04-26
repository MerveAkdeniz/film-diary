namespace FilmDiary.API.DTOs
{
    public class RecommendationResponseDto
    {
        public int FilmId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public decimal ImdbRating { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}