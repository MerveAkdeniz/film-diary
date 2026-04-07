namespace FilmDiary.API.DTOs
{
    public class CommentResponseDto
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsSpoiler { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}