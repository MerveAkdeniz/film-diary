using FilmDiary.API.Models;

namespace FilmDiary.API.Services
{
    public interface IRecommendationService
    {
        Task<List<Film>> GetRecommendationsAsync(int count = 10);
    }
}