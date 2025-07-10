
using E_exam.MapperConfiq;
using E_exam.Models;
using E_exam.Repositories;
using E_exam.Seeds;
using E_exam.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_exam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string txt = "";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<E_examDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("default")));


            //register identity services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddEntityFrameworkStores<E_examDBContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    }
                );
            });


            var app = builder.Build();

            // seed roles and users to datavbase
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                DbUsersInitializer.SeedRolesAndUsersAsync(services).GetAwaiter().GetResult();
            }



            // seed admin and student to database
            //using (var scope = app.Services.CreateScope())
            //{
            //    AdminStudentInitializer.Initialize(scope.ServiceProvider);
            //}
            //########### not working ###########



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors(txt);
            app.MapControllers();

            app.Run();
        }
    }
}
