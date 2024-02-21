using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs
{
    public class ValidateResetTokenDTO
    {
        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; }
    }
}
