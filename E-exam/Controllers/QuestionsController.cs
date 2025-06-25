using AutoMapper;
using E_exam.DTOs.QuestionDTOs;
using E_exam.Models;
using E_exam.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IMapper Mapper;
        private readonly UnitOfWork UnitOfWork;
        public QuestionsController(UnitOfWork UnitOfWork , IMapper Mapper)
        {
            this.UnitOfWork = UnitOfWork;
            this.Mapper = Mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var questions = UnitOfWork.QuestionRepo.GetAll();
            var data = Mapper.Map<List<QuestionDTO>>(questions);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var question = UnitOfWork.QuestionRepo.GetById(id);
            if (question == null)
                return NotFound(new { message = "Question not found." });

            var dto = Mapper.Map<QuestionDTO>(question);
            return Ok(dto);
        }
        [HttpGet("filter")]
        public IActionResult FilterQuestions([FromQuery] int? subjectId, [FromQuery] DifficultyLevel? difficulty)
        {
            var questions = UnitOfWork.QuestionRepo.GetAll();

            if (subjectId.HasValue)
                questions = questions.Where(q => q.SubjectId == subjectId.Value).ToList();

            if (difficulty.HasValue)
                questions = questions.Where(q => q.Difficulty == difficulty.Value).ToList();

            var data = Mapper.Map<List<QuestionDTO>>(questions);
            return Ok(data);
        }


        [HttpPost]
        public IActionResult Create(CreateQuestionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.Type == QuestionType.TrueFalse && dto.Options.Count != 2)
                return BadRequest(new { message = "True/False questions must have exactly 2 options." });

            var question = Mapper.Map<Question>(dto);
            UnitOfWork.QuestionRepo.Add(question);
            UnitOfWork.Save();

            return Ok(new { message = "Question created successfully." });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CreateQuestionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var question = UnitOfWork.QuestionRepo.GetById(id);
            if (question == null)
                return NotFound(new { message = "Question not found." });

            var oldOptions = UnitOfWork.OptionRepo.Db.Options
                .Where(o => o.QuestionId == question.Id)
                .ToList();
            UnitOfWork.OptionRepo.Db.Options.RemoveRange(oldOptions);
            UnitOfWork.Save();

            Mapper.Map(dto, question);
            UnitOfWork.QuestionRepo.Edit(question);

            foreach (var optionDto in dto.Options)
            {
                var newOption = new Option
                {
                    Title = optionDto.Title,
                    IsCorrect = optionDto.IsCorrect,
                    Mark = optionDto.Mark,
                    QuestionId = question.Id
                };
                UnitOfWork.OptionRepo.Add(newOption);
            }

            UnitOfWork.Save();

            return Ok(new { message = "Question updated successfully." });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var question = UnitOfWork.QuestionRepo.GetById(id);
            if (question == null)
                return NotFound(new { message = "Question not found." });

            UnitOfWork.QuestionRepo.Delete(id);
            UnitOfWork.QuestionRepo.Db.SaveChanges();

            return Ok(new { message = "Question deleted successfully." });
        }
    }
}
