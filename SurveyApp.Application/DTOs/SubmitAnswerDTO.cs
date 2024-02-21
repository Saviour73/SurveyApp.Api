using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs
{
    public class SubmitAnswerDTO
    {
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        // You can add more properties if needed
    }
}
