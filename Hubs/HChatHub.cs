using Microsoft.AspNetCore.SignalR;
using TodoApp.Models;
using TodoApp.Repositories;

namespace TodoApp.Hubs
{
    public class HChatHub : Hub
    {
        private readonly ICurrentUserRepo currentUserRepo;
        private readonly DatabaseContext context;

        public HChatHub(ICurrentUserRepo currentUserRepo, DatabaseContext context)
        {
            this.currentUserRepo = currentUserRepo;
            this.context = context;
        }
        public async Task SendMessage(string recieverid, string message)

        {
            var NowDate = DateTime.Now;
            var date = NowDate.ToShortDateString();
            var time = NowDate.ToShortTimeString();


            string SenderId = currentUserRepo.Userid;

            var messageToAdd = new Message()
            {
                Content = message,
                Timestamp = NowDate,
                SenderId = SenderId,
                ReceiverId = recieverid,


            };

            await context.AddAsync(messageToAdd);
            await context.SaveChangesAsync();

            List<string> users = new List<string>()
            {
                recieverid,SenderId
            };

            await Clients.Users(users).SendAsync("ReceiveMessage", message, date, time, SenderId);
        }
    }
}
