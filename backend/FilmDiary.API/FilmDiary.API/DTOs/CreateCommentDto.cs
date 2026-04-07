namespace FilmDiary.API.DTOs
{
    public class CreateCommentDto
    {
        public int FilmId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
