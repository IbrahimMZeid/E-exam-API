using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_exam.Models
{
    public class Exam
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int TotalMarks { get; set; }
        public int DurationInMinites { get; set; }
        public int QuestionsCount { get; set; }
        [Required]
        public int PassMark { get; set; }
        public bool IsPublished { get; set; } = false;
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; } = null!;
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; } = null!;
        public virtual ICollection<StudentExam>? StudentExams { get; set; } = new List<StudentExam>();
        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
        public virtual ICollection<StudentAnswers> StudentAnswers{ get; set; } = new List<StudentAnswers>();

    }
}
