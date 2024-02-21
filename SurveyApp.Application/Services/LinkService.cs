using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SurveyApp.Application.Abstraction.IServices;
using SurveyApp.Dal;
using SurveyApp.Domain.Models;

namespace SurveyApp.Application.Services
{
    public class LinkService : ILinkService
    {
        private readonly AppDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<Survey> _logger;

        public LinkService(AppDatabaseContext context, IMapper mapper, ILogger<Survey> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> GenerateAndSendSurveyLinkOnCreationAsync(int surveyId, string participantEmail)
        {
            var existingSurvey = await _context.Surveys.FirstOrDefaultAsync(s => s.SurveyId == surveyId);

            if (existingSurvey == null)
            {
                throw new Exception("Survey does not exist.");
            }

            string surveyLink = await GenerateSurveyLinkAsync(surveyId);

            // Save the survey link in the database
            await SaveSurveyLinkToDatabaseAsync(surveyId, surveyLink);

            await SendSurveyLinkAsync(surveyLink, participantEmail);

            return surveyLink;
        }

        public async Task<string> GenerateSurveyLinkAsync(int surveyId)
        {
            return $"https://conclaseacademy.com/surveys/{surveyId}";
        }

        public async Task SaveSurveyLinkToDatabaseAsync(int surveyId, string surveyLink)
        {
            var surveyLinkEntity = new SurveyLink
            {
                SurveyId = surveyId,
                Link = surveyLink
            };

            _context.SurveyLinks.Add(surveyLinkEntity);
            await _context.SaveChangesAsync();
        }

        public async Task SendSurveyLinkAsync(string surveyLink, string participantEmail)
        {
            Console.WriteLine($"Sending survey link to {participantEmail}: {surveyLink}");

            await Task.Delay(1000);
        }
    }
}

