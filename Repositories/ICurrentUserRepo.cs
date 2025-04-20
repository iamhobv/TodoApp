using TodoApp.Models;

namespace TodoApp.Repositories
{
    public interface ICurrentUserRepo
    {
        string Userid { get; }
        Task<ApplicationUser> GetUser();
    }
}
