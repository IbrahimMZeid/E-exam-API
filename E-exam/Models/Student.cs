using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_exam.Models
{
    public class Student
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        [StringLength(450)]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser User { get; set; } = null!; 

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = String.Empty;

        public DateTime? DateOfBirth { get; set; } 

        public virtual ICollection<StudentExam>? StudentExams { get; set; } = new List<StudentExam>();
        public virtual ICollection<StudentAnswers> StudentAnswers { get; set; } = new List<StudentAnswers>();
    }
}
