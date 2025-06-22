using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_exam.Models
{
    public class Exam
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        public string CreatedById { get; set; }
        public virtual IdentityUser? CreatedBy { get; set; }
    }
}
