using FilmDiary.API.Data;
using FilmDiary.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddFilm(Film film)
        {
            _context.Films.Add(film);
            await _context.SaveChangesAsync();
            return Ok(film);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFilm(int id, Film updatedFilm)
        {
            var film = await _context.Films.FindAsync(id);

            if (film == null)
            {
                return NotFound("Film bulunamadı.");
            }

            film.Title = updatedFilm.Title;
            film.Overview = updatedFilm.Overview;
            film.Genre = updatedFilm.Genre;
            film.ImdbRating = updatedFilm.ImdbRating;
            film.Status = updatedFilm.Status;

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
