using System.ComponentModel.DataAnnotations;

namespace FilmDiary.API.DTOs
{
    public class CreateCommentDto
    {
        [Required(ErrorMessage = "FilmId zorunludur.")]
        public int FilmId { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        [MinLength(2, ErrorMessage = "Kullanıcı adı en az 2 karakter olmalıdır.")]
        [MaxLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olabilir.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yorum içeriği zorunludur.")]
        [MinLength(5, ErrorMessage = "Yorum en az 5 karakter olmalıdır.")]
        [MaxLength(500, ErrorMessage = "Yorum en fazla 500 karakter olabilir.")]
        public string Content { get; set; } = string.Empty;
    }
}