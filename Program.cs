using Microsoft.EntityFrameworkCore;
using TodoApp.Hubs;
using TodoApp.Models;
using TodoApp.Repositories;

namespace TodoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();



            builder.Services.AddDbContext<DatabaseContext>(
              options =>
              {
                  options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
                  options.EnableSensitiveDataLogging(true);
              }
              );

            builder.Services.AddScoped<IMyTaskRepo, MyTaskRepo>();
            builder.Services.AddScoped<IChatRepo, ChatRepo>();
            builder.Services.AddScoped<ICurrentUserRepo, CurrentUserRepo>();
            builder.Services.AddScoped<IFileRepo, FileRepo>();

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
            })
             .AddEntityFrameworkStores<DatabaseContext>();
            builder.Services.AddSignalR();





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.MapHub<HChatHub>("/HChatHub");
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
