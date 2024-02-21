using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs
{
    public class QuestionResponseDTO
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Type { get; set; }
    }
}
