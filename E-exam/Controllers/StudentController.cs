using AutoMapper;
using E_exam.DTOs.ExamDTOs;
using E_exam.DTOs.ExamQuestionDTO;
using E_exam.DTOs.OptionDTOs;
using E_exam.DTOs.QuestionDTOs;
using E_exam.Models;
using E_exam.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        UnitOfWork unit { get; }
        IMapper mapper { get; }
        public StudentController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet("exams")]
        public IActionResult GetAllAvailableExams() {
            return Ok(mapper.Map<List<ExamListDTO>>(unit.ExamRepo.GetAll()));
        }


        [HttpGet("{id}/TakenExams")]
        public IActionResult GetAllTakenExams(int id)
        {
            var takenExams = unit.StudentExamRepo.GetAllExamsByStudentId(id);
            if (takenExams == null || !takenExams.Any())
            {
                return NotFound("No exams found for the given student ID.");
            }
            return Ok(takenExams);
        }

        [HttpGet("{studentId}/exams/{examId}")]
        public IActionResult GetSpecificExamDetailsForStudent(int studentId, int examId)
        {
            var examState = unit.StudentExamRepo.GetExamStateByStudentIdAndExamId(studentId, examId);
            if (examState == null)
            {
                return NotFound("Exam state not found for the given student ID and exam ID.");
            }
            return Ok(examState);
        }

        [HttpGet("exam/{id}/questions")]
        public IActionResult GetExamQuestions(int id)
        {
            var exam = unit.ExamRepo.GetById(id);
            if (exam == null)
            {
                return NotFound("Exam not found.");
            }

            var examQuestions = unit.ExamQuestionRepo.GetByExamId(id);

            if (examQuestions == null || !examQuestions.Any())
            {
                return NotFound("No questions found for the given exam ID.");
            }

            var result = examQuestions.Select(eq => new QuestionWithOptionsDTO
            {
                Id = eq.Question.Id,
                Title = eq.Question.Title,
                Score = eq.Question.Score,
                Type = eq.Question.Type,
                Difficulty = eq.Question.Difficulty,
                SubjectId = eq.Question.SubjectId,
                SubjectName = eq.Question.Subject.Name,
                Options = eq.Question.Options.Select(opt => new OptionDTO
                {
                    Id = opt.Id,
                    Title = opt.Title,
                    IsCorrect = opt.IsCorrect
                }).ToList()
            });

            return Ok(result);
        }
    }
}
