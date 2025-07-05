using E_exam.Models;
using Microsoft.EntityFrameworkCore;

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

        public List<ExamQuestion> GetByExamId(int examId)
        {
            return Db.ExamQuestion.Where(eq => eq.ExamId == examId)
                .Include(eq => eq.Question)
                .ThenInclude(eq => eq.Options)
                .ToList();
        }
        public void AddRange(int examId, ICollection<int> questionIds)
        {
            List<ExamQuestion> examQuestions = questionIds
                .Select(questionId => new ExamQuestion
                {
                    ExamId = examId,
                    QuestionId = questionId
                }).ToList();
            Db.AddRange(examQuestions);
        }
        public void RemoveRange(int examId, ICollection<int> questionIds)
        {
            // Get the actual tracked entities to remove
            var examQuestions = Db.ExamQuestion
                .Where(eq => eq.ExamId == examId && questionIds.Contains(eq.QuestionId))
                .ToList();
            Db.RemoveRange(examQuestions);
        }
    }
}
