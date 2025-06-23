using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public virtual Exam? Exam { get; set; }
        public virtual List<Option> Options { get; set; } 
        public virtual List<StudentAnswers> StudentAnswers{ get; set; }
        
    }
}
