using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SurveyApp.Application.Abstraction.IServices;
using SurveyApp.Application.DTOs;
using SurveyApp.Dal;
using SurveyApp.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyApp.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<Admin> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;


        public AccountService(AppDatabaseContext context, IMapper mapper,
                ILogger<Admin> logger, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _emailService = emailService;

        }
        public async Task<bool> AdminRegisterServiceAsync(AdminRegisterDTO registerdto)
        {
            try
            {
                if (await _context.Admins.AnyAsync(a => a.Email == registerdto.Email))
                {
                    return false;

                }

                var emailCheck = Validator.TryValidateProperty(registerdto.Email,
                    new ValidationContext(registerdto)
                    {
                        MemberName = nameof(registerdto.Email)
                    }, null

                    );

                var passwordCheck = Validator.TryValidateProperty(registerdto.Password,
                    new ValidationContext(registerdto)
                    {
                        MemberName = nameof(registerdto.Password)
                    }, null

                    );

                var phonenumberCheck = Validator.TryValidateProperty(registerdto.PhoneNumber,
                    new ValidationContext(registerdto)
                    {
                        MemberName = nameof(registerdto.PhoneNumber)
                    }, null

                    );

                if (!emailCheck || !passwordCheck || !phonenumberCheck)
                {
                    return false;
                }

                string hashedPassword = HashPassword(registerdto.Password);
                registerdto.Password = hashedPassword;

                var adminCreated = _mapper.Map<Admin>(registerdto);

                _context.Admins.Add(adminCreated);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        public async Task<string> AdminLoginServiceAsync(AdminLoginDTO logindto)
        {
            try
            {
                var user = await _context.Admins.FirstOrDefaultAsync(u => u.Email == logindto.Email);


                if (user == null)
                {
                    return null;
                }

                var claims = new List<Claim>
                {
                     new Claim("Name", logindto.Email)
                      // Add more claims as needed
                };

                var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_configuration["JWT:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var token = new JwtSecurityToken
                (
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        //private byte[] GenerateKeyBytes()
        //{
        //    // Generate a random key with at least 256 bits
        //    using (var rng = new RNGCryptoServiceProvider())
        //    {
        //        byte[] keyBytes = new byte[32]; // 32 bytes = 256 bits
        //        rng.GetBytes(keyBytes);
        //        return keyBytes;
        //    }
        //}



        public string HashPassword(string password)
        {

            // Generate a salt and hash the password
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        public bool IsValidUser(string email, string password)
        {
            // Implement your user validation logic here
            // For simplicity, this example always returns true
            var user = _context.Admins.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return false;
            }
            return BCrypt.Net.BCrypt.Verify(password, user.Password);

        }


        private string CreateRandomToken()
        {
            Random random = new Random();
            int tokenNumber = random.Next(10000, 100000); // Generates a random number between 10000 and 99999
            return tokenNumber.ToString();
        }


        public async Task<string> ForgotPasswordServiceAsync(string email)
        {
            var user = await _context.Admins.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            // Generate a random token
            string token = CreateRandomToken();

            // Assign the generated token to the user's PasswordResetToken field
            user.PasswordResetToken = token;

            // Set the token expiration time (e.g., 2 minutes from the current time)
            user.ResetTokenExpires = DateTime.Now.AddHours(1);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Send the generated token to the user's email address
            await _emailService.SendPasswordResetTokenAsync(user.Email, token);

            // Return the generated token
            return token;
        }


        public async Task<bool> ValidateResetTokenServiceAsync(ValidateResetTokenDTO validatdto)
        {
            try
            {
                // Check if the user's reset token matches the provided token
                var user = await _context.Admins.FirstOrDefaultAsync(u => u.PasswordResetToken == validatdto.Token);

                if (user == null)
                {
                    // User not found with the given token
                    return false;
                }

                // Check if the token is expired
                if (DateTime.UtcNow > user.ResetTokenExpires)
                {
                    // Token is expired, remove it from the user's record
                    user.PasswordResetToken = null;
                    user.ResetTokenExpires = null;

                    // Update the user's record in the database
                    _context.Update(user);
                    await _context.SaveChangesAsync(); // Save changes to the database asynchronously

                    return false; // Return false as the token is expired
                }
                else
                {
                    // Token is valid and not expired
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }




        public async Task<bool> ResetPasswordServiceAsync(ResetPasswordDTO resetdto)
        {

            var user = await _context.Admins.FirstOrDefaultAsync(u => u.Email == resetdto.Email);
            if (user == null)
            {
                return false;
            }

            if (resetdto.NewPassword != resetdto.ConfirmNewPassword)
            {
                // If the new password and confirmed password don't match, return false
                return false;
            }
            user.Password = resetdto.NewPassword;
            // Hash the new password before storing it in the database
            string hashedPassword = HashPassword(user.Password);

            // Update the user's password in the database
            user.Password = hashedPassword;
            _context.Update(user);
           await _context.SaveChangesAsync();

            // Return true to indicate that the password reset was successful
            return true;

        }
    }
}
