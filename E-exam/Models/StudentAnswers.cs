using System.ComponentModel.DataAnnotations.Schema;

namespace E_exam.Models
{
    public class StudentAnswers
    {
        public int Id { get; set; }
        public int StudentExamId { get; set; }
        public virtual StudentExam? StudentExam { get; set; }
        public int QuestionID { get; set; }
        public virtual Question? Question { get; set; }
        [ForeignKey("SelectedOption")]
        public int SelectedOptionId { get; set; }
        public virtual Option? SelectedOption { get; set; }

        public bool IsCorrect { get; set; }
        public int Mark { get; set; }
    }
}
