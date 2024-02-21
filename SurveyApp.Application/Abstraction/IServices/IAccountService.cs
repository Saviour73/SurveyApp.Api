using SurveyApp.Application.DTOs;


namespace SurveyApp.Application.Abstraction.IServices
{
    public interface IAccountService
    {
        string HashPassword(string password);
        Task<bool> AdminRegisterServiceAsync(AdminRegisterDTO registerdto);
        bool IsValidUser(string email, string password);
        Task<string> AdminLoginServiceAsync(AdminLoginDTO logindto);
        Task<string> ForgotPasswordServiceAsync(string email);
        Task<bool> ValidateResetTokenServiceAsync(ValidateResetTokenDTO validatedto);
        Task<bool> ResetPasswordServiceAsync(ResetPasswordDTO resetdto);

    }
}
