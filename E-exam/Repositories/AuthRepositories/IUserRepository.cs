using E_exam.DTOs.UserDTOs;

namespace E_exam.Repositories
{
    public interface IUserRepository
    {
        List<DisplayedUserDTO> GetAll();
        DisplayedUserDTO GetUserByEmail(string email);
    }
}
