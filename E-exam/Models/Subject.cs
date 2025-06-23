using System.ComponentModel.DataAnnotations;

namespace E_exam.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public List<Exam>? Exams { get; set; }
    }
}
