using System.ComponentModel.DataAnnotations;

namespace E_exam.DTOs.SubjectDTO
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
