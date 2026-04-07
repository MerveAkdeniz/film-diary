using FilmDiary.API.DTOs;

namespace FilmDiary.API.Services
{
    public interface ICommentService
    {
        Task<CommentResponseDto> AddCommentAsync(CreateCommentDto dto);
        Task<List<CommentResponseDto>> GetCommentsByFilmAsync(int filmId);
        Task<List<CommentResponseDto>> GetNonSpoilerCommentsAsync(int filmId);
        Task<List<CommentResponseDto>> GetSpoilerCommentsAsync(int filmId);
    }
}