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
        public override Exam? GetById(int id)
        {
            return Db.Exams
                .Include(e => e.Subject)
                .Include(e => e.Teacher)
                .SingleOrDefault(e => e.Id == id);
        }
    }
}
