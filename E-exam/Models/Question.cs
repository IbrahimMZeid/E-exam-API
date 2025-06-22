using System.ComponentModel.DataAnnotations;

namespace E_exam.Models
{
    public class Question
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Title { get; set; }
        public int Answer { get; set; }
        public int Score { get; set; }
        public bool IsTrueOrFalse { get; set; } = false;
        public int ExamId { get; set; }
        public virtual Exam? Exam { get; set; }
        public virtual List<Answer> Answers{ get; set; } = new List<Answer>();
    }
}
