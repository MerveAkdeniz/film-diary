using FilmDiary.API.Data;
using FilmDiary.API.DTOs;
using FilmDiary.API.Models;
using FilmDiary.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmDiary.API.Common;

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
            return Ok(ApiResponse<object>.SuccessResponse(comments,"Yorumlar başarıyla getirildi."));
        }

        [HttpGet("film/{filmId}/non-spoiler")]
        public async Task<IActionResult> GetNonSpoilerComments(int filmId)
        {
            var comments = await _commentService.GetNonSpoilerCommentsAsync(filmId);
            return Ok(ApiResponse<object>.SuccessResponse(comments,"Spoiler içermeyen yorumlar başarıyla getirildi."));
        }

        [HttpGet("film/{filmId}/spoiler")]
        public async Task<IActionResult> GetSpoilerComments(int filmId)
        {
            var comments = await _commentService.GetSpoilerCommentsAsync(filmId);
            return Ok(ApiResponse<object>.SuccessResponse(comments,"Spoiler içeren yorumlar başarıyla getirildi."));
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.ErrorResponse("Geçersiz veri."));

            var comment = await _commentService.AddCommentAsync(dto);

            return Ok(ApiResponse<object>.SuccessResponse(
                comment,
                "Yorum başarıyla eklendi."
            ));
        }
    }
}