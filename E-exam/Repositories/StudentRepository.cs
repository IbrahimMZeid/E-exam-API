using E_exam.Models;
using Microsoft.EntityFrameworkCore;

namespace E_exam.Repositories
{
    public class StudentRepository : GenericRepository<Student>
    {
        public StudentRepository(E_examDBContext db) : base(db)
        { }
            public int Count()
        {
            return Db.Students.Count() ;
        }
    }
} 


