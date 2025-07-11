using E_exam.Models;
using Microsoft.EntityFrameworkCore;

namespace E_exam.Repositories
{
    public class ExamRepository : GenericRepository<Exam>
    {
        public ExamRepository(E_examDBContext db) : base(db)
        {
        }
        public override ICollection<Exam> GetAll()
        {
            return Db.Exams.Include(e => e.Subject).ToList();
        }
        public int Count()
        {
            return Db.Exams.Count();
        }
        public override Exam? GetById(int id)
        {
            return Db.Exams
                .Include(e => e.Subject)
                .Include(e => e.Teacher)
                .SingleOrDefault(e => e.Id == id);
        }
        public Exam? GetByIdWithQuestions(int id)
        {
            return Db.Exams
                .Include(e => e.ExamQuestions)
                .FirstOrDefault(e => e.Id == id);
        }
        public Exam? GetByIdWithQuestionsAndOptions(int id)
        {
            return Db.Exams
                .Include(e => e.Subject)
                .Include(e => e.ExamQuestions)
                .ThenInclude(eq => eq.Question)
                .ThenInclude(q => q.Options)
                .FirstOrDefault(e => e.Id == id);
        }
    }
}
