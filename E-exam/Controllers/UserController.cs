using E_exam.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace E_exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserRepository repo) : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(repo.GetAll());
        }

        [HttpGet("getuser")]
        public ActionResult GetUserByEmail(string email)
        {
            var displayedUser = repo.GetUserByEmail(email);

            if (displayedUser == null)
                return NotFound("Username not found");

            return Ok(displayedUser);
        }
    }
}
