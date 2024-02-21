using SurveyApp.Application.DTOs;
using SurveyApp.Domain.Models;

namespace SurveyApp.Application.Abstraction.IServices
{
    public interface ISurveyService
    {
        Task<bool> CreateSurveyAsync(CreateSurveyDTO createSurveydto);
        Task<SurveyResponseDTO> GetSurveyByIdAsync(int id);
        Task<List<Survey>> GetAllSurveyAsync();
        Task<bool> UpdateSurveyAsync(int id, UpdateSurveyDto update);
        Task<bool> DeleteSurveyAsync(int id);
    }
}
