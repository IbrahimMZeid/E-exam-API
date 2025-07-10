using E_exam.Models;

namespace E_exam.Seeds
{
    public static class AdminStudentInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<E_examDBContext>();

            await Seed(dbContext);
        }

        private static async Task Seed(E_examDBContext context)
        {
            if (!context.Users.Any())
            {
                // Add your seed users
                context.UsersGG.AddRange(
                    SeedUsers.Admin,
                    SeedUsers.Student
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
