using E_exam.Models;
using Microsoft.EntityFrameworkCore;

namespace E_exam.Repositories
{
    public class StudentExamRepository : GenericRepository<StudentExam>
    {
        public StudentExamRepository(E_examDBContext db) : base(db)
        {

        }

        //return specific exam state for a student by studentId and examId
        public StudentExam? GetExamStateByStudentIdAndExamId(int studentId , int examId)
        {
            return Db.StudentExams
                .Include(se => se.Exam)
                .ThenInclude(e => e.Subject)
                .Include(se => se.Student)
                .FirstOrDefault(se => se.StudentId == studentId && se.ExamId == examId);
        }


        //return all exams for a student by studentId
        public ICollection<StudentExam> GetAllExamsByStudentId(int studentId)
        {
            return Db.StudentExams
                .Include(se => se.Exam)
                .ThenInclude(e => e.Subject)
                .Include(se => se.Student)
                .Where(se => se.StudentId == studentId).ToList();
        }
        public int Count()
        {
            return Db.StudentExams.Count();
        }
    }
}
