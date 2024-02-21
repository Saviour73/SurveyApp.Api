using SurveyApp.Application.DTOs;
using SurveyApp.Domain.Models;

namespace SurveyApp.Application.Abstraction.IServices
{
    public interface ISurveyQuestionService
    {
        Task<bool> CreateQuestionAsync(CreateQuestionDTO createQuestionSurveydto);
        Task<QuestionResponseDTO> GetQuestionByIdAsync(int id);
        Task<List<Question>> GetAllQuestionAsync();
        Task<bool> UpdateQuestionAsync(int id, UpdateQuestionDTO update);
        Task<bool> DeleteQuestionAsync(int id);
    }
}
