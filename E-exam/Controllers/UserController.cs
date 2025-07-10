using E_exam.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace myAuthAPI.Controllers
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
        public ActionResult GetUserByUsername(string username)
        {
            var displayedUser = repo.GetUserByUserName(username);

            if (displayedUser == null)
                return NotFound("Username not found");

            return Ok(displayedUser);
        }
    }
}
