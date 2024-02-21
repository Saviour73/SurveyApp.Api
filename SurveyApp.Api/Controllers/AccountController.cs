using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Abstraction.IServices;
using SurveyApp.Application.DTOs;
using SurveyApp.Domain.Models;

namespace SurveyApp.Api.Controllers
{
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _authRegisterService;
        private readonly IEmailService _emailServices;
        private readonly ILogger<Admin> _logger;
        public AccountController(IAccountService authRegisterService, IEmailService emailServices, ILogger<Admin> logger)
        {
            _authRegisterService = authRegisterService;
            _emailServices = emailServices;
            _logger = logger;
        }



        [HttpPost]
        [Route("registerAdmin")]
        public async Task<IActionResult> AdminRegister([FromBody] AdminRegisterDTO registerdto)
        {

            try

            {
                if (registerdto.ConfirmPassword != registerdto.Password)
                {
                    ModelState.AddModelError("confirm password", "Confirm password must be the same as password");
                    return BadRequest(ModelState);
                }

                var registerAdmin = await _authRegisterService.AdminRegisterServiceAsync(registerdto);

                if (!registerAdmin)
                {
                    ModelState.AddModelError("Email", "Registration Failed");

                    return BadRequest(ModelState);
                }


                return Ok("Admin registered successfully");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while saving the record.");
            }
        }

        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> AdminLogin([FromBody] AdminLoginDTO logindto)
        {
            try
            {
                //// Validate user credentials (implement as needed)
                if (!_authRegisterService.IsValidUser(logindto.Email, logindto.Password))
                {
                    return Unauthorized();
                }

                var result = await _authRegisterService.AdminLoginServiceAsync(logindto);

                if (string.IsNullOrEmpty(result))
                {
                    return Unauthorized();
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while saving the record.");
            }


        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("Email address is required.");
                }
                var result = await _authRegisterService.ForgotPasswordServiceAsync(email);
                if (result == null)
                {
                    return BadRequest("User with this email does not exist.");
                }

                return Ok("Password reset instructions have been sent to your email.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");

            }
        }

        [HttpPost]
        [Route("ValidateResetToken")]
        public async Task<IActionResult> ValidateResetToken(ValidateResetTokenDTO validatedto)
        {
            try
            {
                var isValid = await _authRegisterService.ValidateResetTokenServiceAsync(validatedto);

                if (isValid)
                {
                    // Token is valid, perform the appropriate action
                    return Ok("Token is valid");
                }
                else
                {
                    // Token is not valid or has expired, handle accordingly
                    return BadRequest("Token is not valid or has expired");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Return bad request if model state is invalid
                    return BadRequest(ModelState);
                }

                // Call the ResetPasswordServiceAsync method from the account service
                bool resetSuccess = await _authRegisterService.ResetPasswordServiceAsync(resetdto);

                if (resetSuccess)
                {
                    return Ok(new { message = "Password reset successful" });
                }
                else
                {
                    return BadRequest(new { message = "Password reset failed. Please check your details and try again." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        //[HttpPost("sendSurveyLink")]
        //public async Task<IActionResult> SendSurveyLink([FromBody] EmailDTO emaildto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid email format");
        //    }

        //    var surveyLink = _emailServices.GenerateSurveyLink();
        //    var subject = "Survey invitation";
        //    var body = $"Dear Student, <br /> Please click the link below to fill out the survey form: <br /> <a href = '{surveyLink}'> {surveyLink} </a>";

        //    try
        //    {
        //        await _emailServices.SendEmailAsync(emaildto.Email, subject, body);
        //        return Ok("Survey link send successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return StatusCode(500, "An error occurred while sending survey link." + ex.Message);
        //    }

        //}


    }
}
