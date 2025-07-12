using E_exam.DTOs.UserDTOs;
using E_exam.Models;

namespace E_exam.Repositories
{
    public class UserRepository(E_examDBContext db) : IUserRepository
    {
        public List<DisplayedUserDTO> GetAll()
        {
            List<DisplayedUserDTO> customUsers = db.Users.Select(u => new DisplayedUserDTO()
            {
                id = u.Id.ToString(),
                email = u.Email,
                role = u.Role
            }).ToList();

            return customUsers;
        }

        public DisplayedUserDTO GetUserByEmail(string email)
        {
            User u = db.Users.FirstOrDefault(u => u.Email == email);
            if (u == null)
                return null;

            return new DisplayedUserDTO()
            {
                id = u.Id.ToString(),
                email = u.Email,
                role = u.Role
            };
        }
    }
}
