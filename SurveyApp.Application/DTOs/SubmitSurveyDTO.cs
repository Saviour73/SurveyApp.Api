using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs
{
    public class SubmitSurveyDTO
    {
        public int SurveyId { get; set; }
        public Dictionary<int, string> Answers { get; set; } = new Dictionary<int, string>();
    }
}
