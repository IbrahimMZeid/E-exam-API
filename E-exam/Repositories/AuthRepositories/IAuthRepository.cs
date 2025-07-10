using E_exam.DTOs.UserDTOs;
using E_exam.Models;


namespace E_exam.Repositories
{
    public interface IAuthRepository
    {
        public Task<User> RegisterAsync(UserRegisterDTO userFromReq);
        public Task<string> LoginAsync(UserDTO userFromReq);
        public User? GiveRole(string username, string role);

        //overload to the method to accept json body from request not only query parameters
        public User? GiveRole(UserRoleDTO request);
    }
}
