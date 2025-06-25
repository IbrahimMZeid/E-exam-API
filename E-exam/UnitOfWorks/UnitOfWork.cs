using E_exam.Models;
using E_exam.Repositories;
namespace E_exam.UnitOfWorks
{
    public class UnitOfWork
    {
        private E_examDBContext db;
        private StudentRepository studentRepo;
        private TeacherRepository teacherRepo;
        private ExamRepository examRepo;
        private QuestionRepository questionRepo;
        private StudentAnswersRepository studentAnswerRepo;
        private StudentExamRepository studentExamRepo;
        private SubjectRepository subjectRepo;
        private OptionRepository optionRepo;

        public UnitOfWork(E_examDBContext db)
        {
            this.db = db;
        }
        public StudentRepository StudentRepo
        {
            get
            {
                if (studentRepo == null)
                {
                    studentRepo = new StudentRepository(db);
                }
                return this.studentRepo;
            }
        }
        public TeacherRepository TeacherRepo
        {
            get
            {
                if (teacherRepo == null)
                {
                    teacherRepo = new TeacherRepository(db);
                }
                return this.teacherRepo;
            }
        }
        public ExamRepository ExamRepo
        {
            get
            {
                if (examRepo == null)
                {
                    examRepo = new ExamRepository(db);
                }
                return this.examRepo;
            }
        }
        public QuestionRepository QuestionRepo
        {
            get
            {
                if (questionRepo == null)
                {
                    questionRepo = new QuestionRepository(db);
                }
                return this.questionRepo;
            }
        }
        public StudentAnswersRepository StudentAnswerRepo
        {
            get
            {
                if (studentAnswerRepo == null)
                {
                    studentAnswerRepo = new StudentAnswersRepository(db);
                }
                return this.studentAnswerRepo;
            }
        }
        public StudentExamRepository StudentExamRepo
        {
            get
            {
                if (studentExamRepo == null)
                {
                    studentExamRepo = new StudentExamRepository(db);
                }
                return this.studentExamRepo;
            }
        }
        public SubjectRepository SubjectRepo
        {
            get
            {
                if (subjectRepo == null)
                {
                    subjectRepo = new SubjectRepository(db);
                }
                return this.subjectRepo;
            }
        }
        public OptionRepository OptionRepo
        {
            get
            {
                if (optionRepo == null)
                {
                    optionRepo = new OptionRepository(db);
                }
                return this.optionRepo;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
