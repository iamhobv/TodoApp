using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Repositories;

namespace TodoApp.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMyTaskRepo myTaskRepo;
    private readonly UserManager<ApplicationUser> userManager;

    public HomeController(ILogger<HomeController> logger, IMyTaskRepo myTaskRepo, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        this.myTaskRepo = myTaskRepo;
        this.userManager = userManager;
    }

    public IActionResult Index()
    {

        string user = userManager.GetUserId(User);

        var tasks = myTaskRepo.GetMyTasks(user);
        ViewBag.Tasks = tasks;
        ViewBag.activeTasksCount = tasks.Where(t => t.IsCompleted == false).Count();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
