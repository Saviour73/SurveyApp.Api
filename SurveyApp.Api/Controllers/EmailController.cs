using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Abstraction.IServices;
using SurveyApp.Application.DTOs;

namespace SurveyApp.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailDTO> _logger;
        public EmailController(IEmailService emailService, ILogger<EmailDTO> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }


        [HttpPost]
        [Route("sendEmail")]
        public async Task<IActionResult> SendEmailAsync(EmailDTO emaildto)
        {
            try
            {
                await _emailService.SendEmailAsync(emaildto);

                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

    }
}
