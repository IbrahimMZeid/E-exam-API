using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_exam.Models
{
    public class E_examDBContext : IdentityDbContext<ApplicationUser>
    {
        public E_examDBContext(DbContextOptions<E_examDBContext> options):base(options)
        {   
        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students{ get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<StudentAnswers> StudentAnswers { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestion> ExamQuestion { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Subject> Subjects { get; set; }

    }
}
