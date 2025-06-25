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
        [HttpGet]
        public IActionResult getExam(int id)
        {
            var exam = Unit.ExamRepo.GetById(id);
            if (exam == null)
                return NotFound();
            return Ok(exam);
        }
        [HttpPost]
        public IActionResult Add(Exam exam)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            Unit.ExamRepo.Add(exam);
            Unit.Save();
            return Ok();
        }
        [HttpPut]
        public IActionResult Edit(Exam exam)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            Unit.ExamRepo.Edit(exam); 
            Unit.Save();
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var exam = Unit.ExamRepo.GetById(id);
            if (exam == null)
                return NotFound();
            Unit.ExamRepo.Delete(id);
            Unit.Save();
            return Ok();
        }
    }
}
