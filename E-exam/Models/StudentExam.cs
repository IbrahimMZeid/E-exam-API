using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_exam.Models
{
    [PrimaryKey(nameof(StudentId),nameof(ExamId))]
    public class StudentExam
    {
        public string StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public virtual IdentityUser? Student { get; set; }
        public int ExamId { get; set; }
        public virtual Exam? Exam { get; set; }
        public int Score { get; set; }
    }
}
