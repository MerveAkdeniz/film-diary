using FilmDiary.API.Data;
using FilmDiary.API.DTOs;
using FilmDiary.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmDiary.API.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CommentResponseDto> AddCommentAsync(CreateCommentDto dto)
        {
            var filmExists = await _context.Films.AnyAsync(f => f.Id == dto.FilmId);

            if (!filmExists)
                throw new Exception("Film bulunamadı.");

            var spoilerKeywords = new List<string>
            {
                "finalde",
                "sonunda",
                "ölür",
                "öldü",
                "katil",
                "meğer",
                "aslında",
                "çıktı",
                "ihanet",
                "öldürüyor",
                "ölmesi"
            };

            var contentLower = dto.Content.ToLower();

            var comment = new Comment
            {
                FilmId = dto.FilmId,
                UserName = dto.UserName,
                Content = dto.Content,
                IsSpoiler = spoilerKeywords.Any(keyword => contentLower.Contains(keyword)),
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return new CommentResponseDto
            {
                Id = comment.Id,
                FilmId = comment.FilmId,
                UserName = comment.UserName,
                Content = comment.Content,
                IsSpoiler = comment.IsSpoiler,
                CreatedAt = comment.CreatedAt
            };
        }

        public async Task<List<CommentResponseDto>> GetCommentsByFilmAsync(int filmId)
        {
            return await _context.Comments
                .Where(c => c.FilmId == filmId)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CommentResponseDto
                {
                    Id = c.Id,
                    FilmId = c.FilmId,
                    UserName = c.UserName,
                    Content = c.Content,
                    IsSpoiler = c.IsSpoiler,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<List<CommentResponseDto>> GetNonSpoilerCommentsAsync(int filmId)
        {
            return await _context.Comments
                .Where(c => c.FilmId == filmId && !c.IsSpoiler)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CommentResponseDto
                {
                    Id = c.Id,
                    FilmId = c.FilmId,
                    UserName = c.UserName,
                    Content = c.Content,
                    IsSpoiler = c.IsSpoiler,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<List<CommentResponseDto>> GetSpoilerCommentsAsync(int filmId)
        {
            return await _context.Comments
                .Where(c => c.FilmId == filmId && c.IsSpoiler)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CommentResponseDto
                {
                    Id = c.Id,
                    FilmId = c.FilmId,
                    UserName = c.UserName,
                    Content = c.Content,
                    IsSpoiler = c.IsSpoiler,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();
        }
    }
}