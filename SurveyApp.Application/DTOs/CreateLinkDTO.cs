using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs
{
    public class CreateLinkDTO
    {
        public int SurveyId { get; set; }
        public string ParticipantEmail { get; set; }


    }
}
