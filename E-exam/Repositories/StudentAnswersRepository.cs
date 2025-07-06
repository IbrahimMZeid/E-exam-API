using E_exam.Models;

namespace E_exam.Repositories
{
    public class StudentAnswersRepository : GenericRepository<StudentAnswers>
    {
        public StudentAnswersRepository(E_examDBContext db) : base(db)
        {

        }
        public void AddRange(IEnumerable<StudentAnswers> answers)
        {
            Db.StudentAnswers.AddRange(answers);
        }

    }
}
