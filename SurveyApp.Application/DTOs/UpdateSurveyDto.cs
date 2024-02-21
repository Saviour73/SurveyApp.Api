using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs
{
    public class UpdateSurveyDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string WelcomeMessage { get; set; }
        public string ClosingMessage { get; set; }

        public DateTime ExpirationDate { get; set; }

    }
}
