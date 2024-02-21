namespace SurveyApp.Domain.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Stack { get; set; }
        public List<Answer> Answers { get; set; }

    }
}
