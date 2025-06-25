using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_exam.Models
{
    public class Question
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;
        public int Score { get; set; }
        public QuestionType Type { get; set; } 
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public DifficultyLevel Difficulty { get; set; } = DifficultyLevel.Medium;
        public Subject Subject { get; set; } 
        public virtual ICollection<Option> Options { get; set; } = new List<Option>();
        public virtual ICollection<StudentAnswers> StudentAnswers { get; set; } = new List<StudentAnswers>();
        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();

    }
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
    public enum QuestionType
    {
        MultipleChoice,
        TrueFalse
    }


}
