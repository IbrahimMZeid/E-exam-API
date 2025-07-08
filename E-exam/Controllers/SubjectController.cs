// Controllers/SubjectController.cs
using E_exam.Models;
using E_exam.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace E_exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public SubjectController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var subjects = _unitOfWork.SubjectRepo.GetAll();
            return Ok(subjects);
        }

        [HttpPost]
        public IActionResult Create(Subject subject)
        {
            _unitOfWork.SubjectRepo.Add(subject);
            _unitOfWork.Save();
            return Ok(subject);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Subject subject)
        {
            var existing = _unitOfWork.SubjectRepo.GetById(id);
            if (existing == null)
                return NotFound();

            existing.Name = subject.Name;
            _unitOfWork.SubjectRepo.Edit(existing);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
