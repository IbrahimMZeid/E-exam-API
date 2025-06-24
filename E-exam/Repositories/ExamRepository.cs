using E_exam.Models;

namespace E_exam.Repositories
{
    public class ExamRepository : GenericRepository<Exam>
    {
        public ExamRepository(E_examDBContext db) : base(db)
        {
        }
    }
}
