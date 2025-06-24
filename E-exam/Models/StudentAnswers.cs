namespace E_exam.Models
{
    public class StudentAnswers
    {
        public int Id { get; set; }
        public int StudentExamId { get; set; }
        public virtual StudentExam? StudentExam { get; set; }
        public int QuestionID { get; set; }
        public virtual Question? Question { get; set; }
        public int AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int Mark { get; set; }
    }
}
