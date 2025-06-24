using E_exam.Models;

namespace E_exam.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>
    {
        public TeacherRepository(E_examDBContext db) : base(db)
        {
        }
    }
}
