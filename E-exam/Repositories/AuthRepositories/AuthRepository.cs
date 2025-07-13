using E_exam.DTOs.UserDTOs;
using E_exam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_exam.Repositories
{
    public class AuthRepository(E_examDBContext db, IConfiguration configuration) : IAuthRepository
    {
        public async Task<UserStudentDTO> RegisterAsync(UserRegisterDTO userFromReq)
        {
            if (await db.Users.AnyAsync(u => u.Email == userFromReq.Email))
            {
                return null;
            }

            User user = new User()
            {
                Email = userFromReq.Email,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, userFromReq.Password),
                Role = "student",
                Student = new Student()
                {
                    FirstName = userFromReq.FirstName,
                    LastName = userFromReq.LastName,
                    DateOfBirth = userFromReq.DateOfBirth,
                }
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return new UserStudentDTO()
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role,
                StudentId = user.Student.Id,
                FirstName = user.Student.FirstName,
                LastName = user.Student.LastName,
                DateOfBirth = user.Student.DateOfBirth
            };
        }

        public async Task<string> LoginAsync(UserLoginDTO userFromReq)
        {
            User user = null;

            if (userFromReq.role == "admin")
            {
                user = await db.Users.Include(u => u.Teacher).FirstOrDefaultAsync(u => u.Email == userFromReq.email);
            }
            else if (userFromReq.role == "student")
            {
                user = await db.Users.Include(u => u.Student).FirstOrDefaultAsync(u => u.Email == userFromReq.email);
            }

            if (user is null)
            {
                return null;
            }

            if (user.Role == userFromReq.role)
            {
                if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, userFromReq.password)
                    == PasswordVerificationResult.Failed)
                {
                    return null;
                }
            }
            else
            {
                return null; // Role mismatch
            }

            return CreateToken(user);
        }

        private string CreateToken(User u)
        {
            //1. claims (optional/ mandatory if role based authorization enabled)
            List<Claim> _claims = null;
            if (u.Role == "admin")
            {
                _claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, u.Id.ToString()),
                new Claim(ClaimTypes.Email, u.Email),
                new Claim(ClaimTypes.Role, u.Role),
                new Claim("FirstName", u.Teacher?.FirstName ?? string.Empty),
                new Claim("LastName", u.Teacher?.LastName ?? string.Empty),
                new Claim("TeacherId", u.Teacher?.Id.ToString() ?? string.Empty),
            };
            }
            else if (u.Role == "student")
            {
                _claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, u.Id.ToString()),
                new Claim(ClaimTypes.Email, u.Email),
                new Claim(ClaimTypes.Role, u.Role),
                new Claim("FirstName", u.Student?.FirstName ?? string.Empty),
                new Claim("LastName", u.Student?.LastName ?? string.Empty),
                new Claim("StudentId", u.Student?.Id.ToString() ?? string.Empty),
                new Claim("DateOfBirth", u.Student?.DateOfBirth.Value.ToShortDateString() ?? string.Empty),
            };
            }

            //2. secret key, needs to install package: System.IdentityModel.Tokens.Jwt
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("Token")!));

            //3. hash of secret key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //4. combining token data + expiry date
            var tokenDescriptor = new JwtSecurityToken(
                claims: _claims,
                expires: DateTime.Now.AddDays(20),
                signingCredentials: creds
                );

            //5. generating the token finally
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public User? GiveRole(string email, string role)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email);
            if (user is null)
                return null;

            user.Role = role;
            db.Update(user);
            db.SaveChanges();
            return user;
        }

        public User? GiveRole(UserRoleDTO request)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == request.email);
            if (user is null)
                return null;

            user.Role = request.role;
            db.Update(user);
            db.SaveChanges();
            return user;
        }
    }
}
