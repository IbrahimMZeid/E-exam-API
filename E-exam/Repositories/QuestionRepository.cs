using E_exam.Models;
using Microsoft.EntityFrameworkCore;

namespace E_exam.Repositories
{
    public class QuestionRepository : GenericRepository<Question>
    {
        public QuestionRepository(E_examDBContext db) : base(db)
        {
        }
        public new Question? GetById(int id)
        {
            return Db.Questions
                .Include(q => q.Options)
                .Include(q => q.Subject)
                .FirstOrDefault(q => q.Id == id);
        }
        public ICollection<Question> GetAll()
        {
            return Db.Questions
                .Include (q => q.Options)
                .Include(q => q.Subject)
                .ToList();
    }
}
}
