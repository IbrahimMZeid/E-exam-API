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

        public List<StudentAnswers> GetByStudentIdAndExamId(int studentId, int examId)
        {
             
            return Db.StudentAnswers
                .Where(sa => sa.StudentId == studentId && sa.ExamId == examId)
                .Select(sa => new StudentAnswers
                {
                    StudentId = sa.StudentId,
                    ExamId = sa.ExamId,
                    SelectedOptionId = sa.SelectedOptionId,
                    IsCorrect = sa.IsCorrect,
                    Mark = sa.Mark,
                    QuestionId = sa.QuestionId,
                    Question = new Question
                    {
                        Id = sa.Question.Id,
                        Title = sa.Question.Title,
                        Score = sa.Question.Score,
                        Type = sa.Question.Type,
                        Difficulty = sa.Question.Difficulty,
                        SubjectId = sa.Question.SubjectId,
                        Subject = new Subject
                        {
                            Id = sa.Question.Subject.Id,
                            Name = sa.Question.Subject.Name
                        },
                        Options = sa.Question.Options.Select(opt => new Option
                        {
                            Id = opt.Id,
                            Title = opt.Title,
                            IsCorrect = opt.IsCorrect
                        }).ToList()
                    }
                })
                .ToList();
        }

    }
}
