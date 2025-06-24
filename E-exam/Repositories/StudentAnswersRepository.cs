using E_exam.Models;

namespace E_exam.Repositories
{
    public class StudentAnswersRepository : GenericRepository<StudentAnswers>
    {
        public StudentAnswersRepository(E_examDBContext db) : base(db)
        {
        }
    }
}
