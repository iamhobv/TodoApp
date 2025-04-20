using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Repositories;

namespace TodoApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IChatRepo chatRepo;
        private readonly ICurrentUserRepo currentUserRepo;

        public ChatController(UserManager<ApplicationUser> userManager, IChatRepo chatRepo, ICurrentUserRepo currentUserRepo)
        {
            this.userManager = userManager;
            this.chatRepo = chatRepo;
            this.currentUserRepo = currentUserRepo;
        }

        public async Task<IActionResult> Index()
        {

            var users = chatRepo.getChatsOnly();
            ViewBag.newChat = false;
            return View(users);
        }
        public async Task<IActionResult> NewChat()
        {

            var users = await chatRepo.GetUsers();
            ViewBag.newChat = true;
            return View("Index", users);
        }

        public async Task<IActionResult> Chat(string un)
        {
            if (!string.IsNullOrEmpty(un))
            {
                //ApplicationUser user = await userManager.FindByNameAsync(userName);

                var user = await userManager.FindByNameAsync(un);
                if (user != null)
                {
                    string selecteduserid = user.Id;
                    var chatViewModel = await chatRepo.GetMsgs(selecteduserid);

                    return View(chatViewModel);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
