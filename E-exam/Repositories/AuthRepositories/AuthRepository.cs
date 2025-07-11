﻿using E_exam.DTOs.UserDTOs;
using E_exam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_exam.Repositories
{
    public class AuthRepository(E_examDBContext db, IConfiguration configuration) : IAuthRepository
    {
        public async Task<User> RegisterAsync(UserRegisterDTO userFromReq)
        {
            if (db.UsersGG.Any(u => u.Username == userFromReq.username))
            {
                return null;
            }
            User user = new User();
            user.Email = userFromReq.email;
            user.Username = userFromReq.username;
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, userFromReq.password);
            user.Role = "student";
            db.UsersGG.Add(user);
            await db.SaveChangesAsync();

            return user;
        }

        public async Task<string> LoginAsync(UserDTO userFromReq)
        {
            User user = db.UsersGG.FirstOrDefault(u=>u.Username ==  userFromReq.username);

            if(user is null)
            {
                return null;
            }
            if(new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash,userFromReq.password) 
                == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return CreateToken(user);
        }

        private string CreateToken(User u)
        {
            //1. claims (optional/ mandatory if role based authorization enabled)
            var _claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, u.Id.ToString()),
                new Claim(ClaimTypes.Name, u.Username),
                new Claim(ClaimTypes.Role, u.Role)
            };

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

        public User? GiveRole(string username, string role)
        {
            var user = db.UsersGG.FirstOrDefault(u=>u.Username==username);
            if(user is null)
                return null;

            user.Role = role;
            db.Update(user);
            db.SaveChanges();
            return user;
        }

        public User? GiveRole(UserRoleDTO request)
        {
            var user = db.UsersGG.FirstOrDefault(u => u.Username == request.username);
            if (user is null)
                return null;

            user.Role = request.role;
            db.Update(user);
            db.SaveChanges();
            return user;
        }
    }
}
