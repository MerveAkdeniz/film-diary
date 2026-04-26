using FilmDiary.API.Data;
using FilmDiary.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FilmDiary.API.DTOs;
using FilmDiary.API.Services;
using Microsoft.Extensions.Logging;

namespace FilmDiary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRecommendationService _recommendationService;
        private readonly ILogger<FilmsController> _logger;

        public FilmsController(AppDbContext context, IRecommendationService recommendationService, ILogger<FilmsController> logger)
        {
            _context = context;
            _recommendationService = recommendationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilms(int page = 1, int pageSize = 10)
        {
            var totalCount = await _context.Films.CountAsync();

            var films = await _context.Films
                .OrderByDescending(f => f.ImdbRating)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = films
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilmById(int id)
        {
            var film = await _context.Films.FindAsync(id);

            if (film == null)
            {
                return NotFound("Film bulunamadı.");
            }

            return Ok(film);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchFilms(string title)
        {
            var films = await _context.Films
                .Where(f => f.Title.Contains(title))
                .ToListAsync();

            return Ok(films);
        }
        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            var films = await _context.Films
                .Where(f => f.IsFavorite)
                .ToListAsync();

            return Ok(films);
        }
        [HttpGet("by-actor")]
        public async Task<IActionResult> GetFilmsByActor(string actorName)
        {
            var films = await _context.FilmActors
                .Where(fa => fa.Actor.Name.Contains(actorName))
                .Select(fa => fa.Film)
                .Distinct()
                .ToListAsync();

            return Ok(films);
        }
        [HttpGet("by-two-actors")]
        public async Task<IActionResult> GetFilmsByTwoActors(string actor1, string actor2)
        {
            var actor1FilmIds = await _context.FilmActors
                .Where(fa => fa.Actor.Name.Contains(actor1))
                .Select(fa => fa.FilmId)
                .ToListAsync();

            var actor2FilmIds = await _context.FilmActors
                .Where(fa => fa.Actor.Name.Contains(actor2))
                .Select(fa => fa.FilmId)
                .ToListAsync();

            var commonFilmIds = actor1FilmIds.Intersect(actor2FilmIds).ToList();

            var films = await _context.Films
                .Where(f => commonFilmIds.Contains(f.Id))
                .ToListAsync();

            return Ok(films);
        }
        [HttpGet("{id}/actors")]
        public async Task<IActionResult> GetActorsByFilm(int id)
        {
            var actors = await _context.FilmActors
                .Where(fa => fa.FilmId == id)
                .Select(fa => new
                {
                    fa.ActorId,
                    fa.Actor.Name
                })
                .ToListAsync();

            return Ok(actors);
        }
        [HttpGet("advanced-search")]
        public async Task<IActionResult> AdvancedSearch(
            string? actor1,
            string? actor2,
            string? genre,
            decimal? minRating,
            bool? isFavorite,
            string? status)
        {
            var query = _context.Films
                .Include(f => f.FilmActors)
                .ThenInclude(fa => fa.Actor)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(actor1))
            {
                query = query.Where(f => f.FilmActors.Any(fa => fa.Actor.Name.Contains(actor1)));
            }

            if (!string.IsNullOrWhiteSpace(actor2))
            {
                query = query.Where(f => f.FilmActors.Any(fa => fa.Actor.Name.Contains(actor2)));
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(f => f.Genre.Contains(genre));
            }

            if (minRating.HasValue)
            {
                query = query.Where(f => f.ImdbRating >= minRating.Value);
            }

            if (isFavorite.HasValue)
            {
                query = query.Where(f => f.IsFavorite == isFavorite.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(f => f.Status == status);
            }

            var films = await query
                .Select(f => new
                {
                    f.Id,
                    f.Title,
                    f.Overview,
                    f.Genre,
                    f.ImdbRating,
                    f.Status,
                    f.IsFavorite,
                    Actors = f.FilmActors.Select(fa => fa.Actor.Name).ToList()
                })
                .ToListAsync();

            return Ok(films);
        }
        [HttpGet("recommendations")]
        public async Task<IActionResult> GetRecommendations(int count = 10)
        {
            _logger.LogInformation("Recommendations endpoint çağrıldı");

            var recommendations = await _recommendationService.GetRecommendationsAsync(count);

            return Ok(recommendations);
        }
        [HttpGet("{id}/detail")]
        public async Task<IActionResult> GetFilmDetail(int id)
        {
            var film = await _context.Films
                .Include(f => f.FilmActors)
                    .ThenInclude(fa => fa.Actor)
                .Include(f => f.Comments)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (film == null)
                return NotFound("Film bulunamadı.");

            var dto = new FilmDetailDto
            {
                Id = film.Id,
                Title = film.Title,
                Overview = film.Overview,
                Genre = film.Genre,
                ImdbRating = film.ImdbRating,
                Status = film.Status,
                IsFavorite = film.IsFavorite,
                Actors = film.FilmActors.Select(fa => fa.Actor.Name).ToList(),
                TotalComments = film.Comments.Count,
                SpoilerComments = film.Comments.Count(c => c.IsSpoiler),
                NonSpoilerComments = film.Comments.Count(c => !c.IsSpoiler)
            };

            return Ok(dto);
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
        [HttpPost("{id}/favorite")]
        public async Task<IActionResult> AddToFavorite(int id)
        {
            var film = await _context.Films.FindAsync(id);

            if (film == null)
                return NotFound("Film bulunamadı.");

            film.IsFavorite = true;
            await _context.SaveChangesAsync();

            return Ok("Film favorilere eklendi.");
        }
        [HttpPost("{id}/unfavorite")]
        public async Task<IActionResult> RemoveFromFavorite(int id)
        {
            var film = await _context.Films.FindAsync(id);

            if (film == null)
                return NotFound("Film bulunamadı.");

            film.IsFavorite = false;
            await _context.SaveChangesAsync();

            return Ok("Film favorilerden çıkarıldı.");
        }
        [HttpPost("{filmId}/actors/{actorId}")]
        public async Task<IActionResult> AddActorToFilm(int filmId, int actorId)
        {
            var film = await _context.Films.FindAsync(filmId);
            var actor = await _context.Actors.FindAsync(actorId);

            if (film == null)
                return NotFound("Film bulunamadı.");

            if (actor == null)
                return NotFound("Oyuncu bulunamadı.");

            var exists = await _context.FilmActors
                .AnyAsync(fa => fa.FilmId == filmId && fa.ActorId == actorId);

            if (exists)
                return BadRequest("Bu oyuncu zaten bu filme eklenmiş.");

            var filmActor = new FilmActor
            {
                FilmId = filmId,
                ActorId = actorId
            };

            _context.FilmActors.Add(filmActor);
            await _context.SaveChangesAsync();

            return Ok("Oyuncu filme eklendi.");
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
