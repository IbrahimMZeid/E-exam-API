using E_exam.Models;
using Microsoft.AspNetCore.Identity;

namespace E_exam.Seeds
{
    public static class SeedUsers
    {
        private static readonly Guid AdminUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private static readonly Guid StudentUserId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        public static readonly User Admin = new User
        {
            Id = AdminUserId, // Fixed GUID
            Email = "admin@gmail.com",
            Username = "admin",
            PasswordHash = new PasswordHasher<User>().HashPassword(null, "Admin123!"),
            Role = "Admin"
        };

        public static readonly User Student = new User()
        {
            Id = StudentUserId,
            Email = "student@gmail.com",
            Username = "student1",
            PasswordHash = new PasswordHasher<User>().HashPassword(null, "11"),
            Role = "Student"
        };
    }
}
