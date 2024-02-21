
namespace SurveyApp.Domain.Models
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string WelcomeMessage { get; set; }
        public string ClosingMessage { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public List<Question> Questions { get; set; }

    }
}
