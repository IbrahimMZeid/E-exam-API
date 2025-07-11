using E_exam.DTOs.UserDTOs;
using E_exam.Models;

namespace E_exam.Repositories
{
    public class UserRepository(E_examDBContext db) : IUserRepository
    {
        public List<DisplayedUserDTO> GetAll()
        {
            List<DisplayedUserDTO> customUsers = db.UsersGG.Select(u => new DisplayedUserDTO()
            {
                id = u.Id.ToString(),
                username = u.Username,
                email = u.Email,
                role = u.Role
            }).ToList();

            return customUsers;
        }

        public DisplayedUserDTO GetUserByUserName(string userName)
        {
            User u = db.UsersGG.FirstOrDefault(u => u.Username == userName);
            if (u == null)
                return null;

            return new DisplayedUserDTO()
            {
                id = u.Id.ToString(),
                username = u.Username,
                email = u.Email,
                role = u.Role
            };
        }
    }
}
