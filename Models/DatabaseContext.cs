using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {

        public DbSet<MyTask> Tasks { get; set; }
        public DbSet<Message> Messages { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
