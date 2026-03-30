using FilmDiary.API.Data;
using FilmDiary.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FilmDiary.API.DTOs;
namespace FilmDiary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FilmsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilms()
        {
            var films = await _context.Films.ToListAsync();
            return Ok(films);
        }

        [HttpPost]
        public async Task<IActionResult> AddFilm(CreateFilmDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var film = new Film
            {
                Title = dto.Title,
                Overview = dto.Overview,
                Genre = dto.Genre,
                ImdbRating = dto.ImdbRating,
                Status = dto.Status
            };

            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            return Ok(film);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFilm(int id, UpdateFilmDto dto)
        {
            var film = await _context.Films.FindAsync(id);

            if (film == null)
                return NotFound("Film bulunamadı.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            film.Title = dto.Title;
            film.Overview = dto.Overview;
            film.Genre = dto.Genre;
            film.ImdbRating = dto.ImdbRating;
            film.Status = dto.Status;

            await _context.SaveChangesAsync();

            return Ok(film);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            var film = await _context.Films.FindAsync(id);

            if (film == null)
            {
                return NotFound("Film bulunamadı.");
            }

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();

            return Ok("Film silindi.");
        }
    }
}
