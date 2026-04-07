using FilmDiary.API.Data;
using FilmDiary.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmDiary.API.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly AppDbContext _context;

        public RecommendationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Film>> GetRecommendationsAsync(int count = 10)
        {
            var favoriteGenres = await _context.Films
                .Where(f => f.IsFavorite)
                .Select(f => f.Genre)
                .ToListAsync();

            if (!favoriteGenres.Any())
            {
                return await _context.Films
                    .Where(f => !f.IsFavorite)
                    .OrderByDescending(f => f.ImdbRating)
                    .Take(count)
                    .ToListAsync();
            }

            var mostLikedGenres = favoriteGenres
                .Where(g => !string.IsNullOrWhiteSpace(g))
                .GroupBy(g => g)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .ToList();

            var recommendations = await _context.Films
                .Where(f => !f.IsFavorite && mostLikedGenres.Contains(f.Genre))
                .OrderByDescending(f => f.ImdbRating)
                .Take(count)
                .ToListAsync();

            if (recommendations.Count < count)
            {
                var existingIds = recommendations.Select(f => f.Id).ToList();

                var additionalFilms = await _context.Films
                    .Where(f => !f.IsFavorite && !existingIds.Contains(f.Id))
                    .OrderByDescending(f => f.ImdbRating)
                    .Take(count - recommendations.Count)
                    .ToListAsync();

                recommendations.AddRange(additionalFilms);
            }

            return recommendations;
        }
    }
}