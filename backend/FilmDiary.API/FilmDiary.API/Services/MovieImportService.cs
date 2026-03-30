using CsvHelper;
using CsvHelper.Configuration;
using FilmDiary.API.Data;
using FilmDiary.API.Models;
using System.Globalization;

namespace FilmDiary.API.Services
{
    public class MovieImportService
    {
        private readonly AppDbContext _context;

        public MovieImportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> ImportMoviesAsync(string filePath, int count)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("CSV dosyası bulunamadı.", filePath);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BadDataFound = null,
                MissingFieldFound = null,
                HeaderValidated = null
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);

            var records = csv.GetRecords<MovieCsvRecord>().Take(count).ToList();

            int addedCount = 0;

            foreach (var item in records)
            {
                if (string.IsNullOrWhiteSpace(item.title))
                    continue;

                bool exists = _context.Films.Any(f => f.Title == item.title);
                if (exists)
                    continue;

                decimal imdbRating = 0;
                decimal.TryParse(item.vote_average, NumberStyles.Any, CultureInfo.InvariantCulture, out imdbRating);

                var film = new Film
                {
                    Title = item.title ?? "",
                    Overview = item.overview ?? "",
                    Genre = "Unknown",
                    ImdbRating = imdbRating,
                    Status = "Watchlist"
                };

                _context.Films.Add(film);
                addedCount++;
            }

            await _context.SaveChangesAsync();
            return addedCount;
        }
    }

    public class MovieCsvRecord
    {
        public string title { get; set; } = "";
        public string overview { get; set; } = "";
        public string vote_average { get; set; } = "0";
    }
}