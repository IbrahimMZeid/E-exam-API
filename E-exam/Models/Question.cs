using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_exam.Models
{
    public class Question
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;
        public int Answer { get; set; }
        public int Score { get; set; }
        public bool IsTrueOrFalse { get; set; } = false;
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public virtual Exam? Exam { get; set; }
        public virtual ICollection<Option> Options { get; set; } = new List<Option>();
        public virtual ICollection<StudentAnswers> StudentAnswers { get; set; } = new List<StudentAnswers>();
        
    }
}
