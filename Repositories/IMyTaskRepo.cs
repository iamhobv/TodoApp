using TodoApp.Models;

namespace TodoApp.Repositories
{
    public interface IMyTaskRepo : IGenericRepo<MyTask>
    {
        List<MyTask> GetMyTasks(string userId);
        List<MyTask> TaskWithFilter(string filter, string userID);

        public bool SoftDelete(int id);
        public bool TaskCheked(int id);
        public bool TaskUncheked(int id);

    }
}
