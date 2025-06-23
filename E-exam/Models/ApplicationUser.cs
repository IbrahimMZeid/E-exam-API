using Microsoft.AspNetCore.Identity;

namespace E_exam.Models
{
    public class ApplicationUser :IdentityUser
    {
        public Teacher? TeacherProfile { get; set; }
        public Student? StudentProfile { get; set; }
    }
}
