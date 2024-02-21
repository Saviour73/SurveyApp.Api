using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using SurveyApp.Application.Abstraction.IServices;
using SurveyApp.Application.DTOs;

namespace SurveyApp.Application.Services
{
    public class EmailService : IEmailService
    {


        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(EmailDTO emaildto)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value));
                email.To.Add(MailboxAddress.Parse(emaildto.To));
                email.Subject = emaildto.Subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emaildto.Body };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                await smtp.ConnectAsync(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task SendPasswordResetTokenAsync(string email, string token)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value)); // Set your application's name and email address
                message.To.Add(new MailboxAddress(null, email)); // Set the recipient's email address
                message.Subject = "Password Reset Token";

                // Compose the email body with the password reset token
                message.Body = new TextPart("html")
                {
                    Text = $@"
                            <html>
                             <body>
                                    <p>You have requested a password reset for your account. Please use the following token to reset your password:</p>
                                    <p><strong>Token:</strong> {token}</p>
                                    <p>This token will expire after 1 hour.</p>
                                    <p>If you did not request a password reset, please ignore this email or contact support immediately.</p>
                    </body>
                    </html>"
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    // Connect to the SMTP server (replace with your SMTP server settings)
                    await client.ConnectAsync(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);

                    // Authenticate with the SMTP server (replace with your authentication credentials)
                    await client.AuthenticateAsync(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);

                    // Send the email message
                    await client.SendAsync(message);

                    // Disconnect from the SMTP server
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }



        //private readonly string _smtpHost;
        //private readonly int _smtpPort;
        //private readonly string _smtpUsername;
        //private readonly string _smtpPassword;

        //public EmailServices(string smtpHost, int smtpPort, string smtpUsername, string smtpPassword)
        //{
        //    _smtpHost = smtpHost;
        //    _smtpPort = smtpPort;
        //    _smtpUsername = smtpUsername;
        //    _smtpPassword = smtpPassword;
        //}
        //public async Task SendEmailAsync(string email, string subject, string body)
        //{
        //    using (var client = new SmtpClient(_smtpHost, _smtpPort))
        //    {
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
        //        client.EnableSsl = true;

        //        var message = new MailMessage(_smtpUsername, email, subject, body);
        //        message.IsBodyHtml = true;

        //        await client.SendMailAsync(message);
        //    }
        //}

        //public string GenerateSurveyLink()
        //{
        //    return "https://conclasesurveyapp.com/surveys/123";
        //}

    }
}
