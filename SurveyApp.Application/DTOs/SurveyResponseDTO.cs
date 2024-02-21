using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs
{
    public class SurveyResponseDTO
    {
        public int SurveyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string WelcomeMessage { get; set; }
        public string ClosingMessage { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int AdminId { get; set; }
        public bool IsSuccess { get; set; } = true;
    }
}
