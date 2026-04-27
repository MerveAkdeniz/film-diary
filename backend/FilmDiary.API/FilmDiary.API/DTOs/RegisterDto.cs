using System.ComponentModel.DataAnnotations;

namespace FilmDiary.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MinLength(4)]
        public string Password { get; set; } = string.Empty;
    }
}