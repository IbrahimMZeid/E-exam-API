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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // 🔄 Relation: Question - Options
            builder.Entity<Option>()
                .HasOne(o => o.Question)
                .WithMany(q => q.Options)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Question>()
                .HasOne(q => q.Subject)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);


            // 🔄 Relation: Question - ExamQuestion
            //builder.Entity<ExamQuestion>()
            //    .HasOne(eq => eq.Question)
            //    .WithMany(q => q.ExamQuestions)
            //    .HasForeignKey(eq => eq.QuestionId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<ExamQuestion>()
            //    .HasOne(eq => eq.Exam)
            //    .WithMany(e => e.ExamQuestions)
            //    .HasForeignKey(eq => eq.ExamId)
            //    .OnDelete(DeleteBehavior.Cascade);

            // 🔄 Relation: Question - StudentAnswers
            //builder.Entity<StudentAnswers>()
            //    .HasOne(sa => sa.Question)
            //    .WithMany(q => q.StudentAnswers)
            //    .HasForeignKey(sa => sa.QuestionID)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<StudentAnswers>()
            //    .HasOne(sa => sa.SelectedOption)
            //    .WithMany()
            //    .HasForeignKey(sa => sa.SelectedOptionId)
            //    .OnDelete(DeleteBehavior.NoAction); 

            //builder.Entity<StudentAnswers>()
            //    .HasOne(sa => sa.StudentExam)
            //    .WithMany(se => se.StudentAnswers)
            //    .HasForeignKey(sa => sa.StudentExamId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// 🔄 Relation: Exam - StudentExam
            //builder.Entity<StudentExam>()
            //    .HasOne(se => se.Exam)
            //    .WithMany(e => e.StudentExams)
            //    .HasForeignKey(se => se.ExamId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<StudentExam>()
            //    .HasOne(se => se.Student)
            //    .WithMany(s => s.StudentExams)
            //    .HasForeignKey(se => se.StudentId)
            //    .OnDelete(DeleteBehavior.Cascade);



            //// 🔄 Relation: Subject - Exam
            //builder.Entity<Exam>()
            //    .HasOne(e => e.Subject)
            //    .WithMany(s => s.Exams)
            //    .HasForeignKey(e => e.SubjectId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// 🔄 Relation: Exam - Teacher
            //builder.Entity<Exam>()
            //    .HasOne(e => e.Teacher)
            //    .WithMany(t => t.Exams)
            //    .HasForeignKey(e => e.TeacherId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
