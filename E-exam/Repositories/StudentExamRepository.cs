using E_exam.Models;

namespace E_exam.Repositories
{
    public class StudentExamRepository : GenericRepository<StudentExam>
    {
        public StudentExamRepository(E_examDBContext db) : base(db)
        {
        }
    }
}
