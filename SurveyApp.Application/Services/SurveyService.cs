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
    public class SurveyService : ISurveyService
    {
        private readonly IBaseRepository<Survey> _surveyRepository;
        private readonly AppDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<Survey> _logger;

        public SurveyService(IBaseRepository<Survey> surveyRepository, AppDatabaseContext context, IMapper mapper,ILogger<Survey> logger)
        {
            _surveyRepository = surveyRepository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateSurveyAsync(CreateSurveyDTO createSurveydto)
        {
            try
            {
                var admin = await _context.Admins.FirstOrDefaultAsync(a => a.AdminId == createSurveydto.AdminId);

                if (admin == null)
                {
                    return false;
                }

                var survey = new Survey
                {
                    AdminId = createSurveydto.AdminId,
                    Title = createSurveydto.Title,
                    Description = createSurveydto.Description,
                    WelcomeMessage = createSurveydto.WelcomeMessage,
                    ClosingMessage = createSurveydto.ClosingMessage,
                    ExpirationDate = createSurveydto.ExpirationDate
                };

                //or use mapper
                //var surveyCreated = _mapper.Map<Survey>(createSurveydto);

                _context.Surveys.Add(survey);
                await _context.SaveChangesAsync();


                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteSurveyAsync(int id)
        {
            try
            {

                var existingSurvey = await _surveyRepository.GetByIdAsync(id);

                if (existingSurvey == null)
                {
                    return false;
                }

                _surveyRepository.Delete(existingSurvey);
                await _surveyRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<Survey>> GetAllSurveyAsync()
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

        public async Task<SurveyResponseDTO> GetSurveyByIdAsync(int id)
        {
            var survey = await _surveyRepository.GetByIdAsync(id);

            if (survey == null)
            {
                return null;
            }
            return new SurveyResponseDTO
            {
                AdminId = survey.AdminId,
                SurveyId = survey.SurveyId,
                Title = survey.Title,
                Description = survey.Description,
                WelcomeMessage = survey.WelcomeMessage,
                ClosingMessage = survey.ClosingMessage,
                ExpirationDate = survey.ExpirationDate
            };
        
        }

        public  async Task<bool> UpdateSurveyAsync(int id, UpdateSurveyDto update)
        {
            try
            {
                var existingSurvey = await _context.Surveys.FirstOrDefaultAsync(s => s.SurveyId == id);

                if (existingSurvey == null)
                {
                    return false;
                }

                existingSurvey.Title = update.Title;
                existingSurvey.Description = update.Description;
                existingSurvey.WelcomeMessage = update.WelcomeMessage;
                existingSurvey.ClosingMessage = update.ClosingMessage;
                existingSurvey.ExpirationDate = update.ExpirationDate;

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
