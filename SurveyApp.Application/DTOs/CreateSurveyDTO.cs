using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs
{
    public class CreateSurveyDTO
    {
        [Required]
        public int AdminId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string WelcomeMessage { get; set; }
        [Required]
        public string ClosingMessage { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }
        




    }
}
