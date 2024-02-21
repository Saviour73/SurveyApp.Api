using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs
{
    public class CreateQuestionDTO
    {
        [Required]
        public int AdminId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int SurveyId { get; set; }
    }
}
