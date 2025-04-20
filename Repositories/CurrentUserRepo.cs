using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Repositories
{
    public class CurrentUserRepo : ICurrentUserRepo
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly DatabaseContext context;

        public CurrentUserRepo(IHttpContextAccessor httpContextAccessor, DatabaseContext context)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
        }
        public string Userid
        {
            get
            {
                var id = httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
                if (string.IsNullOrEmpty(id))
                {
                    id = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                return id ?? string.Empty;
            }
        }


        public async Task<ApplicationUser?> GetUser()
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == Userid);
            return user;
        }
    }
}
