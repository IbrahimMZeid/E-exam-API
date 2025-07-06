using AutoMapper;
using E_exam.DTOs.ExamDTOs;
using E_exam.DTOs.OptionDTOs;
using E_exam.DTOs.QuestionDTOs;
using E_exam.DTOs.StudentExamDTO;
using E_exam.Models;
using E_exam.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult GetAllAvailableExams()
        {
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

            var result = mapper.Map<List<StudentExamResultDTO>>(takenExams);
            return Ok(result);
        }


        [HttpGet("{studentId}/exams/{examId}")]
        public IActionResult GetSpecificExamDetailsForStudent(int studentId, int examId)
        {
            var examState = unit.StudentExamRepo.GetExamStateByStudentIdAndExamId(studentId, examId);
            if (examState == null)
            {
                return NotFound("Exam state not found for the given student ID and exam ID.");
            }
            var dto = new StudentExamDetailsDTO
            {
                StudentId = examState.StudentId,
                ExamId = examState.ExamId,
                StudentName = examState?.Student?.FirstName + " " + examState?.Student?.LastName,
                ExamTitle = examState.Exam.Name,
                Subject = examState.Exam.Subject.Name,
                TotalScore = examState.Exam.TotalMarks,
                Score = examState.Score,
                Passed = examState.Passed
            };
            return Ok(dto);
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

        [HttpPost("{studentId}/exams/{examId}/submit")]
        public IActionResult SubmitExam(int studentId, int examId, [FromBody] ExamSubmissionDTO submission)
        {
            if (submission == null || submission.Answers == null || !submission.Answers.Any())
                return BadRequest("No answers submitted.");

            var answers = submission.Answers;

            var exam = unit.ExamRepo.GetById(examId);
            if (exam == null)
                return NotFound("Exam not found.");

            var student = unit.StudentRepo.GetById(studentId);
            if (student == null)
                return NotFound("Student not found.");

            // check if the student has already submitted this exam
            var existingStudentExam = unit.StudentExamRepo.GetExamStateByStudentIdAndExamId(studentId, examId);
            if (existingStudentExam != null && existingStudentExam.Score > 0)
                return BadRequest("You have already submitted this exam.");

            int totalMark = 0;
            List<StudentAnswers> studentAnswersList = new();

            foreach (var optionId in answers)
            {
                var option = unit.OptionRepo
                    .Db.Set<Option>()
                    .Include(o => o.Question)
                    .FirstOrDefault(o => o.Id == optionId);

                if (option == null || option.Question == null)
                    continue;

                bool isCorrect = option.IsCorrect;
                int mark = isCorrect ? option.Question.Score : 0;
                totalMark += mark;

                studentAnswersList.Add(new StudentAnswers
                {
                    StudentId = studentId,
                    ExamId = examId,
                    SelectedOptionId = optionId,
                    IsCorrect = isCorrect,
                    Mark = mark,
                });
            }

            if (!studentAnswersList.Any())
                return BadRequest("No valid answers found.");

            unit.StudentAnswerRepo.AddRange(studentAnswersList);

           
            
            bool passed = totalMark >= exam.PassMark; 

            // save in StudentExam
            if (existingStudentExam != null)
            {
                existingStudentExam.Score = totalMark;
                existingStudentExam.Passed = passed;
                unit.StudentExamRepo.Edit(existingStudentExam);
            }
            else
            {
                unit.StudentExamRepo.Add(new StudentExam
                {
                    StudentId = studentId,
                    ExamId = examId,
                    Score = totalMark,
                    Passed = passed
                });
            }

            unit.Save();

            return Ok(new
            {
                Message = "Exam submitted successfully.",
                TotalMark = totalMark,
                Passed = passed
            });
        }



    }
}
