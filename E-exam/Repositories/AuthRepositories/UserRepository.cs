using E_exam.DTOs.UserDTOs;
using E_exam.Models;
using Microsoft.EntityFrameworkCore;

namespace E_exam.Repositories
{
    public class UserRepository(E_examDBContext db) : IUserRepository
    {
        public List<DisplayedUserDTO> GetAll()
        {
            var customUsers = db.Users.Include(u => u.Teacher).Include(u => u.Student).Select(u => new DisplayedUserDTO()
            {
                id = u.Id.ToString(),
                name = u.Role.ToLower().Equals("admin")? $"{u.Teacher.FirstName} {u.Teacher.LastName}": $"{u.Student.FirstName} {u.Student.LastName}",
                role = u.Role
            }).ToList();

            return customUsers;
        }

        public DisplayedUserDTO GetUserByEmail(string email)
        {
            User u = db.Users.Include(u => u.Teacher).Include(u => u.Student).FirstOrDefault(u => u.Email == email);
            if (u == null)
                return null;

            return new DisplayedUserDTO()
            {
                id = u.Id.ToString(),
                name = u.Role.ToLower().Equals("admin") ? $"{u.Teacher.FirstName} {u.Teacher.LastName}" : $"{u.Student.FirstName} {u.Student.LastName}",
                role = u.Role
            };
        }
    }
}
