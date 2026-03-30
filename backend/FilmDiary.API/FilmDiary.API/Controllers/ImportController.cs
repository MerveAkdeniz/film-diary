using FilmDiary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmDiary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly MovieImportService _movieImportService;

        public ImportController(MovieImportService movieImportService)
        {
            _movieImportService = movieImportService;
        }

        [HttpPost("movies")]
        public async Task<IActionResult> ImportMovies(int count = 100)
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "..", "..", "..",
                "the-movies-dataset",
                "movies_metadata.csv"
            );

            if (!System.IO.File.Exists(filePath))
            {
                return BadRequest($"CSV dosyası bulunamadı. Aranan yol: {filePath}");
            }

            var addedCount = await _movieImportService.ImportMoviesAsync(filePath, count);

            return Ok($"{addedCount} film eklendi.");
        }
    }
}