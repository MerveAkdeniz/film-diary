using FilmDiary.API.Data;
using FilmDiary.API.DTOs;
using FilmDiary.API.Models;
using FilmDiary.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmDiary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("film/{filmId}")]
        public async Task<IActionResult> GetCommentsByFilm(int filmId)
        {
            var comments = await _commentService.GetCommentsByFilmAsync(filmId);
            return Ok(comments);
        }

        [HttpGet("film/{filmId}/non-spoiler")]
        public async Task<IActionResult> GetNonSpoilerComments(int filmId)
        {
            var comments = await _commentService.GetNonSpoilerCommentsAsync(filmId);
            return Ok(comments);
        }

        [HttpGet("film/{filmId}/spoiler")]
        public async Task<IActionResult> GetSpoilerComments(int filmId)
        {
            var comments = await _commentService.GetSpoilerCommentsAsync(filmId);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto dto)
        {
            try
            {
                var comment = await _commentService.AddCommentAsync(dto);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}