using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Abstraction.IServices
{
    public interface ILinkService
    {
        Task<string> GenerateAndSendSurveyLinkOnCreationAsync(int surveyId, string participantEmail);
        Task<string> GenerateSurveyLinkAsync(int surveyId);
        Task SendSurveyLinkAsync(string surveyLink, string participantEmail);


    }
}