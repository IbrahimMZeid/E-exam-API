// Controllers/SubjectController.cs
using AutoMapper;
using E_exam.DTOs.SubjectDTO;
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
        IMapper Mapper { get; }

        public SubjectController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var subjects = _unitOfWork.SubjectRepo.GetAll();
            return Ok(Mapper.Map<List<SubjectDTO>>(subjects));
        }

        [HttpPost]
        public IActionResult Create(SubjectDTO subjectFromRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newSubject = Mapper.Map<Subject>(subjectFromRequest);
            _unitOfWork.SubjectRepo.Add(newSubject);
            _unitOfWork.Save();
            return Ok(Mapper.Map<SubjectDTO>(newSubject));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, SubjectDTO subjectFromRequest)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            if(subjectFromRequest.Id != id)
                return BadRequest(new {message = "Invalid id"});
            var existing = _unitOfWork.SubjectRepo.GetById(id);
            if (existing == null)
                return NotFound();

            Mapper.Map(subjectFromRequest, existing);
            //_unitOfWork.SubjectRepo.Edit(existing);
            _unitOfWork.Save();

            return Ok(Mapper.Map<SubjectDTO>(existing));
        }
    }
}
