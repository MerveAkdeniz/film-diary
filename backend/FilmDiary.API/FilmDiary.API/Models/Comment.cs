using System.Text.Json.Serialization;

namespace FilmDiary.API.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int FilmId { get; set; }

        [JsonIgnore]
        public Film? Film { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public bool IsSpoiler { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}