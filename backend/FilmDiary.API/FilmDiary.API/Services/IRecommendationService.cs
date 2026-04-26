using FilmDiary.API.DTOs;

namespace FilmDiary.API.Services
{
    public interface IRecommendationService
    {
        Task<List<RecommendationResponseDto>> GetRecommendationsAsync(int count = 10);
    }
}