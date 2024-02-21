using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Utilities
{
    public static class ServerResponseCode
    {
        public static string InvalidAdminIdCode { get; } = "0005";
        public static string InvalidAdminIdMessage { get; } = "Invalid Admin ID";
        public static string AdminFoundCode { get; } = "0007";
        public static string AdminFoundMessage { get; } = "Admin Found";
         public static string ServerErrorCode { get; } = "0009";
         public static string ServerErrorMessage { get; } = "Insufficient Books";


    }
}
