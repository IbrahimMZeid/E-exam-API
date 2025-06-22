using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_exam.Models
{
    public class E_examDBContext : IdentityDbContext
    {
        public E_examDBContext(DbContextOptions<E_examDBContext> options):base(options)
        {   
        }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<StudentExam> StudentExams{ get; set; }
        public DbSet<Question> Questions{ get; set; }
        public DbSet<Answer> Answers{ get; set; }
    }
}
