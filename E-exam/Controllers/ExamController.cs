using E_exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        public E_examDBContext Db { get; }
        public ExamController(E_examDBContext db)
        {
            Db = db;
        }
        [HttpGet]
        public IActionResult getExams()
        {
            var exams = Db.Exams.ToList();
            return Ok(exams);
        }
    }
}
