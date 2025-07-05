using E_exam.Models;

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
            return Db.StudentExams.FirstOrDefault(se => se.StudentId == studentId && se.ExamId == examId);
        }

        //return all exams for a student by studentId
        public ICollection<StudentExam> GetAllExamsByStudentId(int studentId)
        {
            return Db.StudentExams.Where(se => se.StudentId == studentId).ToList();
        }
    }
}
