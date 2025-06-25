using AutoMapper;
using E_exam.DTOs.ExamDTOs;
using E_exam.Models;
using E_exam.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace E_exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        public UnitOfWork Unit { get; }
        public IMapper Mapper { get; }
        public ExamController(UnitOfWork unit, IMapper mapper)
        {
            Unit = unit;
            Mapper = mapper;
        }


        [HttpGet]
        public IActionResult getExams()
        {
            var exams = Unit.ExamRepo.GetAll();
            return Ok(Mapper.Map<List<ExamListDTO>>(exams));
        }
        [HttpGet("{id}")]
        public IActionResult getExam(int id)
        {
            var exam = Unit.ExamRepo.GetById(id);
            if (exam == null)
                return NotFound();
            return Ok(Mapper.Map<ExamListDTO>(exam));

        }
        [HttpPost]
        public IActionResult Add(ExamFormDTO examFromReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var exam = Mapper.Map<Exam>(examFromReq);
            Unit.ExamRepo.Add(exam);
            Unit.Save();
            return Ok(new { message = "Exam Added Successfully." });
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, ExamFormDTO examFromReq)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var oldExam = Unit.ExamRepo.GetById(id);
            if (oldExam == null)
                return NotFound(new { message = "Exam not found" });
            var exam = Mapper.Map<Exam>(examFromReq);
            Unit.ExamRepo.Edit(exam);
            Unit.Save();
            return Ok(new { message = "Exam Updateed Successfully." });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var exam = Unit.ExamRepo.GetById(id);
            if (exam == null)
                return NotFound(new { message = "Exam not found" });
            var examQuestions = Unit.ExamQuestionRepo.getExamQuestions(id);
            if (examQuestions != null)
                Unit.ExamQuestionRepo.RemoveRange(examQuestions);
            Unit.ExamRepo.Delete(id);
            Unit.Save();
            return Ok(new { message = "Exam Deleted Successfully" });
        }
    }
}
