using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Utilities
{
    public class ServerResponse
    {
        public string code { get; set; } = "0000";
        public string message { get; set; }
        public object data { get; set; }
    }
}
