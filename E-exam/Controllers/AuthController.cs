using E_exam.DTOs.UserDTOs;
using E_exam.Models;
using E_exam.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace myAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepository userRepo) : ControllerBase
    {
        public static User myUser = new();

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserRegisterDTO userFromReq)
        {
            User user = await userRepo.RegisterAsync(userFromReq);
            if (user == null)
                return BadRequest("User already exists!");

            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDTO userFromReq)
        {
            if (userFromReq == null || userFromReq.username is null)
            {
                return NotFound("Username or password is incorrect!");
                //return Content("enter username and password like this: /login?username=x&password=y");
            }
            string token = await userRepo.LoginAsync(userFromReq);
            if (token is null)
                return NotFound("Username or password is incorrect!");

            return Ok(token);
        }

        [HttpGet("AuthOnly")]
        [Authorize]
        public IActionResult AuthOnly()
        {
            return Content("You Are Authorized");
            ;
        }

        [HttpPut("giveroleQueryParams")]
        [Authorize(Roles = "Admin")]
        public IActionResult GiveRole(string username, string role)
        {
            var user = userRepo.GiveRole(username, role);
            if (user == null)
                return NotFound("user not found");

            return Ok(user);
        }

        [HttpPut("giverole")]
        [Authorize(Roles = "Admin")]
        public IActionResult GiveRole([FromBody] UserRoleDTO request)
        {
            var user = userRepo.GiveRole(request);
            if (user == null)
                return NotFound("user not found");

            return Ok(user);
        }
    }
}
