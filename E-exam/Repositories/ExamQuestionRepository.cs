using E_exam.Models;

namespace E_exam.Repositories
{
    public class ExamQuestionRepository : GenericRepository<ExamQuestion>
    {
        public ExamQuestionRepository(E_examDBContext db) : base(db)
        {
        }
        public IQueryable<ExamQuestion> getExamQuestions(int id)
        {
            return Db.ExamQuestion.Where(eq => eq.ExamId == id);
        }
        public void RemoveRange(IQueryable<ExamQuestion> list)
        {
            Db.RemoveRange(list);
        }
    }
}
