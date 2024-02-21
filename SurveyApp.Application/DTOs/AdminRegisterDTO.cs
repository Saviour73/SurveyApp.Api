using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SurveyApp.Application.DTOs
{
    public class AdminRegisterDTO
    {
        //[Required(ErrorMessage = "Email is required")]
        //[EmailAddress(ErrorMessage = "Invalid Email address format")]
        //[RegularExpression(@"^[a-zA-Z0-9._%+-]+@conclase\.com$", ErrorMessage = "Only Conclase address are allowed")]
        public string Email { get; set; }

        [Required(ErrorMessage = " Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()><|{}]).{8,}$",
            ErrorMessage = "Password must contain at least one capital letter, " +
            "one small letter, one special character: !@#$%^&*()<>?|], and a minimum of eight characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password required")]
        [Compare("Password", ErrorMessage = "Confirm password must match the password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]

        public string Lastname { get; set; }

        [Required] 
        [StringLength(11, ErrorMessage = "Phone Number must have a maximum length of 11")]
        [RegularExpression("^(080|081|070|091|090)[0-9]*$", ErrorMessage = "Phone Number must follow righr format and must be 11 dights.")]
        public string PhoneNumber { get; set; }

    }
}
