using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_exam.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string? LastName { get; set; } = string.Empty;

        public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
