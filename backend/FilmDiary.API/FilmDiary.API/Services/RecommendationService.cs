using FilmDiary.API.Data;
using FilmDiary.API.DTOs;
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

        public async Task<List<RecommendationResponseDto>> GetRecommendationsAsync(int count = 10)
        {
            var favoriteGenres = await _context.Films
                .Where(f => f.IsFavorite && !string.IsNullOrWhiteSpace(f.Genre))
                .Select(f => f.Genre)
                .ToListAsync();

            if (!favoriteGenres.Any())
            {
                return await _context.Films
                    .Where(f => !f.IsFavorite)
                    .OrderByDescending(f => f.ImdbRating)
                    .Take(count)
                    .Select(f => new RecommendationResponseDto
                    {
                        FilmId = f.Id,
                        Title = f.Title,
                        Genre = f.Genre,
                        ImdbRating = f.ImdbRating,
                        Reason = "Genel yüksek puanlı filmler arasından önerildi."
                    })
                    .ToListAsync();
            }

            var mostLikedGenres = favoriteGenres
                .GroupBy(g => g)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .ToList();

            var recommendations = await _context.Films
                .Where(f => !f.IsFavorite && mostLikedGenres.Any(g => f.Genre.Contains(g)))
                .OrderByDescending(f => f.ImdbRating)
                .Take(count)
                .Select(f => new RecommendationResponseDto
                {
                    FilmId = f.Id,
                    Title = f.Title,
                    Genre = f.Genre,
                    ImdbRating = f.ImdbRating,
                    Reason = "Favori filmlerindeki tür tercihlerine göre önerildi."
                })
                .ToListAsync();

            if (recommendations.Count < count)
            {
                var existingIds = recommendations.Select(f => f.FilmId).ToList();

                var additionalFilms = await _context.Films
                    .Where(f => !f.IsFavorite && !existingIds.Contains(f.Id))
                    .OrderByDescending(f => f.ImdbRating)
                    .Take(count - recommendations.Count)
                    .Select(f => new RecommendationResponseDto
                    {
                        FilmId = f.Id,
                        Title = f.Title,
                        Genre = f.Genre,
                        ImdbRating = f.ImdbRating,
                        Reason = "Ek öneri olarak yüksek puanlı filmler arasından seçildi."
                    })
                    .ToListAsync();

                recommendations.AddRange(additionalFilms);
            }

            return recommendations;
        }
    }
}