using E_exam.Models;

namespace E_exam.Repositories
{
    public class StudentRepository : GenericRepository<Student>
    {
        public StudentRepository(E_examDBContext db) : base(db)
        {
        }
    }
}
