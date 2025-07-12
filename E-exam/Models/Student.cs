using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_exam.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User? User { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<StudentExam>? StudentExams { get; set; } = new List<StudentExam>();
        public virtual ICollection<StudentAnswers>? StudentAnswers { get; set; } = new List<StudentAnswers>();
    }
}
