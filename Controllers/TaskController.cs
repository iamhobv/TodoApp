using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using TodoApp.Models;
using TodoApp.Repositories;
using TodoApp.ViewModels;
using static TodoApp.Models.Enums;

namespace TodoApp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMyTaskRepo myTaskRepo;

        public TaskController(UserManager<ApplicationUser> userManager, IMyTaskRepo myTaskRepo)
        {
            this.userManager = userManager;
            this.myTaskRepo = myTaskRepo;
        }
        [HttpGet]

        public IActionResult AddTask()
        {
            string userId = userManager.GetUserId(User);
            if (userId == null)
            {
                userId = "not Found";
            }
            ViewBag.userId = userId;
            ViewBag.taskCategories = Enum.GetValues(typeof(Category));
            ViewBag.users = userManager.Users.ToList();

            return View("AddTask");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTask(NewTaskViewModel newTask)
        {
            string userId = userManager.GetUserId(User);
            if (userId == null)
            {
                userId = "not Found";
            }
            ViewBag.userId = userId;
            ViewBag.taskCategories = Enum.GetValues(typeof(Category));
            ViewBag.users = userManager.Users.ToList();
            //if (newTask.Category == 0)
            //{
            //    ModelState.AddModelError("Category", "Please select valid category");
            //}



            if (ModelState.IsValid)
            {
                MyTask task = new MyTask()
                {
                    Content = newTask.Content,
                    Category = newTask.Category,
                    AssignToUserId = newTask.AssignToUserId,
                    CreatedUserId = newTask.CreatedUserId,
                    DeadLine = newTask.DeadLine,
                    IsCompleted = newTask.IsCompleted,
                    IsDeleted = newTask.IsDeleted,
                    Status = Enums.TaskStatus.Active
                };

                if (!string.IsNullOrEmpty(newTask.AssignToUserId))
                {
                    if (!newTask.AssignToUserId.Equals("0"))
                    {
                        task.AssignToUserId = newTask.AssignToUserId;

                    }
                    else
                    {
                        task.AssignToUserId = null;
                    }
                }



                myTaskRepo.Insert(task);
                myTaskRepo.Save();
                return RedirectToAction("Index", "Home");


            }
            return View("AddTask", newTask);
        }


        public IActionResult DeleteTask(int id)
        {
            myTaskRepo.SoftDelete(id);
            myTaskRepo.Save();
            return Json(new
            {
                result = "success"
            });

        }
        public IActionResult TaskChekedComplete(int id)
        {
            myTaskRepo.TaskCheked(id);
            myTaskRepo.Save();
            return Json(new
            {
                result = "success"
            });

        }
        public IActionResult TaskChekedUncomplete(int id)
        {
            myTaskRepo.TaskUncheked(id);
            myTaskRepo.Save();
            return Json(new
            {
                result = "success"
            });

        }


        public IActionResult TaskFilters(string filter)
        {
            string userId = userManager.GetUserId(User);

            var tasks = myTaskRepo.TaskWithFilter(filter, userId);

            return Json(tasks);

        }

    }
}
