
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Abstraction.IRepositories;
using SurveyApp.Application.Abstraction.IServices;
using SurveyApp.Application.Helpers;
using SurveyApp.Application.Services;
using SurveyApp.Dal.Repositories;

namespace SurveyApp.Api.Registrars
{
    public class MVCRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
           
            builder.Services.AddEndpointsApiExplorer();

           

            //builder.Services.AddScoped<IEmailService>(provider =>
            //{
            //     var smtpHost = "smtp.gmail.com";

            //    var smtpPort = 587;

            //    var smtpUsername = "Palmfitsquad15@gmail.com";

            //    var smtpPassword = "kwatusdniiwfygmr";


            //   return new EmailServices(smtpHost, smtpPort, smtpUsername, smtpPassword);   
            //});
            builder.Services.AddScoped<ISurveyAnswerService, SurveyAnswerService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(SurveyRepository<>));
            builder.Services.AddScoped<ISurveyService, SurveyService>();
            builder.Services.AddScoped<ISurveyQuestionService, SurveyQuestionService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ILinkService, LinkService>();





        }
    }
}
