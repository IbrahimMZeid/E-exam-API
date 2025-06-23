using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_exam.Models
{
    [PrimaryKey(nameof(StudentId),nameof(ExamId))]
    public class StudentExam
    {
        [ForeignKey("Student")]
        public string StudentId { get; set; }

        public virtual Student Student { get; set; }
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public virtual Exam? Exam { get; set; }
        public int Score { get; set; }
        public bool Passed { get; set; }
        public List<StudentAnswers> StudentAnswers { get; set; }
    }
}
