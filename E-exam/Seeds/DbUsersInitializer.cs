using E_exam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_exam.Seeds
{
    public static class DbUsersInitializer
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<E_examDBContext>();

            var teachers = new[]
            {
                new { Email = "teacher1@gradely.com", Password = "teacher1@123", FirstName = "Sara", LastName = "Hassan" },
                new { Email = "teacher2@gradely.com", Password = "teacher2@123", FirstName = "Mohamed", LastName = "Youssef" },
                new { Email = "teacher3@gradely.com", Password = "teacher3@123", FirstName = "Fatma", LastName = "Ibrahim" }
            };

            foreach (var t in teachers)
            {
                // Check if user exists
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == t.Email);
                if (user == null)
                {
                    user = new User()
                    {
                        Email = t.Email,
                        PasswordHash = new PasswordHasher<User>().HashPassword(null, t.Password),
                        Role = "admin",
                        Teacher = new Teacher()
                        {
                            FirstName = t.FirstName,
                            LastName = t.LastName,
                        }
                    };
                    dbContext.Users.Add(user);
                    await dbContext.SaveChangesAsync();
                }

                //// Check if teacher exists
                //var teacherExists = await dbContext.Teachers.AnyAsync(x => x.UserId == user.Id);
                //if (!teacherExists)
                //{
                //    var teacher = new Teacher
                //    {
                //        UserId = user.Id,
                //        FirstName = t.FirstName,
                //        LastName = t.LastName,
                //        Email = t.Email // If Teacher has Email property
                //        // Add other properties as needed
                //    };
                //    dbContext.Teachers.Add(teacher);
                //    await dbContext.SaveChangesAsync();
                //}
            }
        }
    }
}
