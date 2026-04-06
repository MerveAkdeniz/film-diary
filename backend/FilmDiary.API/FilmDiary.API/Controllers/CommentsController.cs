using FilmDiary.API.Data;
using FilmDiary.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmDiary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CommentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("film/{filmId}")]
        public async Task<IActionResult> GetCommentsByFilm(int filmId)
        {
            var comments = await _context.Comments
                .Where(c => c.FilmId == filmId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            var filmExists = await _context.Films.AnyAsync(f => f.Id == comment.FilmId);

            if (!filmExists)
                return NotFound("Film bulunamadı.");

            comment.CreatedAt = DateTime.UtcNow;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }
    }
}