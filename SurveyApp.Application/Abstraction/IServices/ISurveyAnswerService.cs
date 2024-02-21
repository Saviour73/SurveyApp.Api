using SurveyApp.Application.DTOs;
using SurveyApp.Domain.Models;

namespace SurveyApp.Application.Abstraction.IServices
{
    public interface ISurveyAnswerService
    {
        Task<bool> SubmitAnswerAsync(SubmitAnswerDTO submitAnswerDTO);
        Task<List<Answer>> GetAnswersForQuestionAsync(int questionId);
        // You can add more methods as needed
    }
}
