using Microsoft.EntityFrameworkCore;
using System.Linq;
using TaskManager.Common.Models;

namespace TaskManager.API.Models.Data
{
    public class ApplicationContext: DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<ProjectAdmin> ProjectAdmins { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
            if(Users.Any(u => u.Status == UserStatus.Admin) == false)
            {
                var admin = new User("Ivan", "Ivanov", "admin@gmail.com", "qwerty", UserStatus.Admin, "898534224405", new byte[0]);
                Users.Add(admin);
                try
                {
                    SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }

            }
        }

    }
}
