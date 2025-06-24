using E_exam.Models;

namespace E_exam.Repositories
{
    public class QuestionRepository : GenericRepository<Question>
    {
        public QuestionRepository(E_examDBContext db) : base(db)
        {
        }
    }
}
