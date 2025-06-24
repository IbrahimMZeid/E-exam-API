using E_exam.Models;

namespace E_exam.Repositories
{
    public class OptionRepository : GenericRepository<Option>
    {
        public OptionRepository(E_examDBContext db) : base(db)
        {
        }
    }
}
