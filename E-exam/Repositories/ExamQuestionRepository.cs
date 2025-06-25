using E_exam.Models;

namespace E_exam.Repositories
{
    public class ExamQuestionRepository : GenericRepository<ExamQuestion>
    {
        public ExamQuestionRepository(E_examDBContext db) : base(db)
        {
        }
    }
}
