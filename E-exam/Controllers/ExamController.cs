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
        [HttpGet("count")]
        public IActionResult ExamCount()
        {
            int count = Unit.ExamRepo.Count();
            return Ok(count);
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
            var exam = Unit.ExamRepo.GetByIdWithQuestionsAndOptions(id);
            if (exam == null)
                return NotFound();
            return Ok(Mapper.Map<ExamDisplayDTO>(exam));
        }

        [HttpGet("GetForEdit/{id}")]
        public IActionResult GetForEdit(int id)
        {
            var exam = Unit.ExamRepo.GetByIdWithQuestionsAndOptions(id);
            if (exam == null)
                return NotFound();
            return Ok(Mapper.Map<ExamFormDTO>(exam));
        }

        [HttpPost]
        public IActionResult Add(ExamFormDTO examFromReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var exam = Mapper.Map<Exam>(examFromReq);
            exam.QuestionsCount = examFromReq.ExamQuestions.Count;
            Unit.ExamRepo.Add(exam);
            Unit.Save();
            return Ok(new { message = "Exam Added Successfully." });
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, ExamFormDTO examFromReq)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != examFromReq.Id)
                return BadRequest(new { message = "Id Not Matched" });
            var oldExam = Unit.ExamRepo.GetByIdWithQuestions(id);
            if (oldExam == null)
                return NotFound(new { message = "Exam not found" });

            Mapper.Map(examFromReq, oldExam);
            oldExam.QuestionsCount = examFromReq.ExamQuestions.Count;

            if (examFromReq.ExamQuestions != null && examFromReq.ExamQuestions.Any())
            {
                var currentQuestions = oldExam.ExamQuestions.Select(q => q.QuestionId);
                // Get new questions to add it
                var questionsToAdd = examFromReq.ExamQuestions.Except(currentQuestions).ToList();
                // Get old questions that are not in examFromReq to remove it
                 var questionsToRemove = currentQuestions.Except(examFromReq.ExamQuestions).ToList();
                // Add new questions
                if (questionsToAdd.Any())
                {
                    Unit.ExamQuestionRepo.AddRange(id, questionsToAdd);
                }
                // Remove old questions
                if (questionsToRemove.Any())
                {
                    Unit.ExamQuestionRepo.RemoveRange(id, questionsToRemove);
                }
            }
            Unit.Save();
            return Ok(new { message = "Exam Updateed Successfully." });
        }
        [HttpDelete("{id}")]
        public IActionResult TogglePublish(int id)
        {
            var exam = Unit.ExamRepo.GetById(id);
            if (exam == null)
                return NotFound(new { message = "Exam not found" });
            //var examQuestions = Unit.ExamQuestionRepo.getExamQuestions(id);
            //if (examQuestions != null)
            //    Unit.ExamQuestionRepo.RemoveRange(examQuestions);
            exam.IsPublished = !exam.IsPublished;
            //Unit.ExamRepo.Delete(id);
            Unit.Save();
            return Ok(new { message = "Exam Publish State Toggled Successfully" });
        }
    }
}
