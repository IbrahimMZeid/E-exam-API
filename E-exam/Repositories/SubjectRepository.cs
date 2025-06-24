using E_exam.Models;

namespace E_exam.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>
    {
        public SubjectRepository(E_examDBContext db) : base(db)
        {
        }
    }
}
