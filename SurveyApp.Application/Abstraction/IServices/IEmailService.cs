using SurveyApp.Application.DTOs;

namespace SurveyApp.Application.Abstraction.IServices
{
    public interface IEmailService
    {
        //Task SendEmailAsync(string email, string subject, string body);
        //string GenerateSurveyLink();

        Task SendEmailAsync(EmailDTO emaildto);
        Task SendPasswordResetTokenAsync(string email, string token);
    }
}
