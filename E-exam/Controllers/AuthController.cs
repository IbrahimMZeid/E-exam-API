using E_exam.DTOs.UserDTOs;
using E_exam.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace E_exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepository userRepo, IUserRepository userData) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserRegisterDTO userFromReq)
        {
            UserStudentDTO user = await userRepo.RegisterAsync(userFromReq);
            if (user == null)
                return BadRequest("User already exists!");

            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserLoginDTO userFromReq)
        {
            if (userFromReq == null || userFromReq.email is null)
            {
                return NotFound("Username or password is incorrect!");
            }
            var user = userData.GetUserByEmail(userFromReq.email);
            string token = await userRepo.LoginAsync(userFromReq);
            if (token is null)
                return NotFound("Username or password is incorrect!");

            return Ok( new {token, user });
        }

        // This endpoint allows giving a role to a user by passing a JSON body in the request
        [HttpPut("Give_Role")]
        [Authorize(Roles = "Admin,admin")]
        public IActionResult GiveRole([FromBody] UserRoleDTO request)
        {
            var user = userRepo.GiveRole(request);
            if (user == null)
                return NotFound("user not found");

            return Ok(user);
        }
    }
}
