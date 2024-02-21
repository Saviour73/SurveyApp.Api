namespace SurveyApp.Domain.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string? PasswordResetToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        public List<Survey> Surveys { get; set; }
        

    }
}
