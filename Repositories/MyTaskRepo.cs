using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Models;

namespace TodoApp.Repositories
{
    public class MyTaskRepo : GenericRepo<MyTask>, IMyTaskRepo
    {
        private readonly DatabaseContext context;
        //private readonly List<MyTask> tasks;

        public MyTaskRepo(DatabaseContext context) : base(context)
        {
            this.context = context;
            //tasks = new List<MyTask>();

        }



        public List<MyTask> GetMyTasks(string userId)
        {
            List<MyTask> tasks = new List<MyTask>();

            var t3 = context.Tasks
                .Where(t => ((t.CreatedUserId.Contains(userId) && string.IsNullOrEmpty(t.AssignToUserId)) || (t.AssignToUserId.Contains(userId))) && t.IsDeleted == false).Include(t => t.CreatedUser).OrderBy(t => t.IsCompleted).ThenBy(t => t.DeadLine).ToList(); //t => t.IsCompleted

            //var t2 = context.Tasks.Where(t => ((t.CreatedUserId.Contains(userId) && string.IsNullOrEmpty(t.AssignToUserId)) && t.IsDeleted == false)).OrderBy(t => t.DeadLine).ThenBy(t => t.IsCompleted).ToList();

            //tasks.AddRange(t2);
            //var t1 = context.Tasks.Where(t => t.AssignToUserId.Contains(userId) && t.IsDeleted == false).Include(t => t.CreatedUser).OrderBy(t => t.DeadLine).ThenBy(t => t.IsCompleted).ToList();

            //tasks.AddRange(t1);


            //tasks.OrderBy(t => t.IsCompleted).ToList();
            return t3;


        }


        public List<MyTask> TaskWithFilter(string filter, string userID)
        {
            var tasks = GetMyTasks(userID);
            if (filter.Contains("All"))
            {
                return tasks;

            }
            else if (filter.Contains("Active"))
            {
                return tasks.Where(t => t.IsCompleted == false).ToList();
            }
            else if (filter.Contains("Complete"))
            {
                return tasks.Where(t => t.IsCompleted == true).ToList();

            }
            return tasks;




        }

        public bool SoftDelete(int id)
        {
            MyTask item = GetById(id);
            if (item != null)
            {
                item.IsDeleted = true;
                context.Update(item);
                return true;
            }
            return false;
        }


        public bool TaskCheked(int id)
        {
            MyTask item = GetById(id);
            if (item != null)
            {
                item.IsCompleted = true;
                item.Status = Enums.TaskStatus.Complete;
                context.Update(item);
                return true;
            }
            return false;

        }
        public bool TaskUncheked(int id)
        {
            MyTask item = GetById(id);
            if (item != null)
            {
                item.IsCompleted = false;
                item.Status = Enums.TaskStatus.Active;
                context.Update(item);
                return true;
            }
            return false;
        }

    }
}
