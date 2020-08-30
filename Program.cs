using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SchoolApi.Models;
using SchoolApi.Repository;

namespace SchoolApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                SchoolContext ctx = services.GetRequiredService<SchoolContext>();
                if (ctx != null)
                {
                    ctx.Courses.AddRange(new CoursePoco() { Id = 1, Name = "Course1" }, 
                        new CoursePoco() { Id = 2, Name = "Course2" },
                        new CoursePoco() { Id = 3, Name = "Course3" });
                    ctx.Students.AddRange(new StudentPoco()
                    {
                        Id = 1,
                        Name = "Student1",
                        Age = 23,
                        Enrollments = new EnrollmentPoco[1] { new EnrollmentPoco() { StudentId = 1, CourseId = 2 } }.ToList()
                    }, new StudentPoco() { Id = 2, Name = "Student2", Age=35,
                        Enrollments = new EnrollmentPoco[1] { new EnrollmentPoco() { StudentId = 2, CourseId = 2 } }.ToList()
                    });

                    ctx.Enrollments.AddRange(new EnrollmentPoco() { StudentId  = 2, CourseId = 1},
                        new EnrollmentPoco() { StudentId = 2, CourseId = 3 });
                    ctx.SaveChanges();

                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                /*.ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })*/
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
