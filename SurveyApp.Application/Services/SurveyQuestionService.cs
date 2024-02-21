using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SurveyApp.Application.Abstraction.IRepositories;
using SurveyApp.Application.Abstraction.IServices;
using SurveyApp.Application.DTOs;
using SurveyApp.Dal;
using SurveyApp.Domain.Models;

namespace SurveyApp.Application.Services
{
    public class SurveyQuestionService : ISurveyQuestionService
    {
        private readonly IBaseRepository<Question> _surveyRepository;
        private readonly AppDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<Question> _logger;

        public SurveyQuestionService(IBaseRepository<Question> surveyRepository, AppDatabaseContext context, IMapper mapper, ILogger<Question> logger)
        {
            _surveyRepository = surveyRepository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateQuestionAsync(CreateQuestionDTO createQuestiondto)
        {
            try
            {
                var admin = await _context.Admins.FirstOrDefaultAsync(a => a.AdminId == createQuestiondto.AdminId);

                if (admin == null)
                {
                    return false;
                }

                var survey = await _context.Surveys.FirstOrDefaultAsync(s => s.SurveyId == createQuestiondto.SurveyId);

                if (survey == null)
                {
                    // Handle the case where the survey does not exist
                    return false;
                }


                var question = new Question
                {
                    Text = createQuestiondto.Text,
                    Type = createQuestiondto.Type,
                    SurveyId = createQuestiondto.SurveyId
                };


                _context.Questions.Add(question);
                await _context.SaveChangesAsync();


                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            try
            {

                var existingQuestion = await _surveyRepository.GetByIdAsync(id);

                if (existingQuestion == null)
                {
                    return false;
                }

                _surveyRepository.Delete(existingQuestion);
                await _surveyRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        public async Task<List<Question>> GetAllQuestionAsync()
        {
            try
            {
                return await _surveyRepository.GetAll();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        public async Task<QuestionResponseDTO> GetQuestionByIdAsync(int id)
        {
            var question = await _surveyRepository.GetByIdAsync(id);

            if (question == null)
            {
                return null;
            }
            return new QuestionResponseDTO
            {
                QuestionId = question.QuestionId,
                Text = question.Text,
            };


        }


        public async Task<bool> UpdateQuestionAsync(int id, UpdateQuestionDTO update)
        {
            try
            {
                var existingQuestion = await _context.Questions.FirstOrDefaultAsync(s => s.QuestionId == id);

                if (existingQuestion == null)
                {
                    return false;
                }

                existingQuestion.Text = update.Text;
                existingQuestion.Type = update.Type;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


    }
}
