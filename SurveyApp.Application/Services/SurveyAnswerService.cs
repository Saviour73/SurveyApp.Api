using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SurveyApp.Application.Abstraction.IServices;
using SurveyApp.Application.DTOs;
using SurveyApp.Dal;
using SurveyApp.Domain.Models;

namespace SurveyApp.Application.Services
{
    public class SurveyAnswerService : ISurveyAnswerService
    {
        private readonly AppDatabaseContext _context;
        private readonly ILogger<Question> _logger;

        public SurveyAnswerService( AppDatabaseContext context, ILogger<Question> logger)
        {
            _context = context;
            _logger = logger;
        }



        public async Task<bool> SubmitAnswerAsync(SubmitAnswerDTO submitAnswerDTO)
        {
            try
            {
                var question = await _context.Questions.FirstOrDefaultAsync(q => q.QuestionId == submitAnswerDTO.QuestionId);

                if (question == null)
                {
                    return false;
                }

                var answer = new Answer
                {
                    QuestionId = submitAnswerDTO.QuestionId,
                    Text = submitAnswerDTO.AnswerText,
                };

                _context.Answers.Add(answer);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<Answer>> GetAnswersForQuestionAsync(int questionId)
        {
            try
            {
                var answers = await _context.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
                return answers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


    }
}
