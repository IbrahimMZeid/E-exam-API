using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_exam.Models
{
    public class Teacher
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        [StringLength(450)] 
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; } = null!; 

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public List<Exam>? Exams { get; set; }
    }
}
