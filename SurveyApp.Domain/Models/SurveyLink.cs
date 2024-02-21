using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Models
{
    public class SurveyLink
    {
        public int SurveyLinkId { get; set; }
        public int SurveyId { get; set; }
        public string Link { get; set; }
    }

}
