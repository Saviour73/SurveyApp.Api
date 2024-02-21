
namespace SurveyApp.Domain.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; } // Navigation property for the associated question
                                               // Add any additional properties for the answer entity
    }
}
