using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_exam.Models
{
    public class Exam
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public int TotalMarks { get; set; }

        [Required]
        public int PassMark { get; set; }

        public bool IsPublished { get; set; } = false;

        [ForeignKey("Questions")]
        public int QuestionsId { get; set; }
        public List<Question>? Questions { get; set; }

        [ForeignKey("StudentExams")]
        public int StudentExamsId { get; set; }
        public List<StudentExam>? StudentExams { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;


        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

    }
}
