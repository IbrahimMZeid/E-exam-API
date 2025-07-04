using E_exam.Models;
using Microsoft.AspNetCore.Identity;

namespace E_exam.Seeds
{
    public static class DbUsersInitializer
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var dbContext = serviceProvider.GetRequiredService<E_examDBContext>();

           
            string[] roles = { "Admin", "Student" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var teacherEmail = "admin@gradely.com";
            var teacher = await userManager.FindByEmailAsync(teacherEmail);
            if (teacher == null)
            {
                var teacherUser = new ApplicationUser
                {
                    UserName = teacherEmail,
                    Email = teacherEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(teacherUser, "admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(teacherUser, "Admin");

                    var teacherProfile = new Teacher
                    {
                        UserId = teacherUser.Id,
                        FirstName = "Ahmed",
                        LastName = "Ali"
                    };

                    dbContext.Teachers.Add(teacherProfile);
                    await dbContext.SaveChangesAsync();
                }
            }

            
            var studentEmail = "student@gradely.com";
            var student = await userManager.FindByEmailAsync(studentEmail);
            if (student == null)
            {
                var studentUser = new ApplicationUser
                {
                    UserName = studentEmail,
                    Email = studentEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(studentUser, "student@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(studentUser, "Student");

                    var studentProfile = new Student
                    {
                        UserId = studentUser.Id,
                        FirstName = "Yassin",
                        LastName = "Mansour",
                        DateOfBirth = new DateTime(2000, 5, 1)
                    };

                    dbContext.Students.Add(studentProfile);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
