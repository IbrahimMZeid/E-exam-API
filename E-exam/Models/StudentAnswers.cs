namespace E_exam.Models
{
    public class StudentAnswers
    {
        public int id { get; set; }
        public int StudentExamId { get; set; }
        public StudentExam StudentExam { get; set; }
        public int QuestionID { get; set; }
        public Question Question { get; set; }

        public int AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int Mark { get; set; }



    }
}
