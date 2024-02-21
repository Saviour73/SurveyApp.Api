
namespace SurveyApp.Domain.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public List<Answer> Answers { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}
