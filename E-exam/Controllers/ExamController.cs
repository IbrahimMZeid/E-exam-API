using E_exam.Models;
using E_exam.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        public ExamController(UnitOfWork unit)
        {
            Unit = unit;
        }

        public UnitOfWork Unit { get; }

        [HttpGet]
        public IActionResult getExams()
        {
            var exams = Unit.ExamRepo.GetAll();
            return Ok(exams);
        }
    }
}
