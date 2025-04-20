using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Repositories
{
    public class ChatRepo : IChatRepo
    {
        private readonly ICurrentUserRepo currentUserRepo;
        private readonly DatabaseContext context;

        public ChatRepo(ICurrentUserRepo currentUserRepo, DatabaseContext context)
        {
            this.currentUserRepo = currentUserRepo;
            this.context = context;
        }

        public List<LastMsgViewModel> getChatsOnly()
        {
            var currentuserid = currentUserRepo.Userid;

            var users = context.Users
                .Where(e => e.Id != currentuserid)
                .Select(e => new LastMsgViewModel
                {
                    Id = e.Id,

                    UserName = e.UserName,

                    ProfilePicPath = e.ProfilePicUrl,

                    LastMsgDate = context.Messages
                        .Where(m =>
                            ((m.Sender.Id == currentuserid && m.ReceiverId == e.Id) ||
                            (m.Sender.Id == e.Id && m.ReceiverId == currentuserid)))
                        .OrderByDescending(m => m.Timestamp)
                        .Select(m => m.Timestamp)
                        .FirstOrDefault(),

                    LastMsg = context.Messages
                        .Where(m =>
                            ((m.Sender.Id == currentuserid && m.ReceiverId == e.Id) ||
                            (m.Sender.Id == e.Id && m.ReceiverId == currentuserid)))
                        .OrderByDescending(m => m.Timestamp)
                        .Select(m => m.Content)
                        .FirstOrDefault()
                }).ToList().Where(m => !string.IsNullOrEmpty(m.LastMsg)).ToList();


            return users;
        }

        public async Task<ChatViewModel> GetMsgs(string SeletedUserid)
        {
            var currentid = currentUserRepo.Userid;
            var selectesuser = await context.Users.FirstOrDefaultAsync(i => i.Id == SeletedUserid);
            var selectedusername = "";
            if (selectesuser != null)
            {
                selectedusername = selectesuser.UserName;
            }

            var messsages = await context.Messages
                .Where(i => (i.Sender.Id == currentid || i.Sender.Id == SeletedUserid) &&
                            (i.ReceiverId == currentid || i.ReceiverId == SeletedUserid))
                .OrderBy(i => i.Timestamp)
                .Select(i => new MsgsInsideChat()
                {
                    Id = i.Id,
                    Message = i.Content,
                    //parsedDateTime.ToString("MM/dd/yyyy hh:mm tt");

                    Date = i.Timestamp.ToString("MM/dd/yyyy hh:mm tt"),
                    IsCurrentUserSentMsg = i.Sender.Id == currentid,
                })
                .ToListAsync();

            var chatView = new ChatViewModel()
            {
                CurrentUserId = currentid,
                ReceiverId = SeletedUserid,
                ReceiverUserName = selectedusername,
                Msgs = messsages,
                ProfilePicPath = selectesuser?.ProfilePicUrl
            };

            return chatView;
        }

        public async Task<List<LastMsgViewModel>> GetUsers()
        {
            var currentuserid = currentUserRepo.Userid;

            var users = await context.Users
                .Where(e => e.Id != currentuserid)
                .Select(e => new LastMsgViewModel
                {
                    Id = e.Id,
                    UserName = e.UserName,
                    ProfilePicPath = e.ProfilePicUrl,
                    LastMsg = context.Messages
                        .Where(m =>
                            (m.Sender.Id == currentuserid && m.ReceiverId == e.Id) ||
                            (m.Sender.Id == e.Id && m.ReceiverId == currentuserid))
                        .OrderByDescending(m => m.Timestamp)
                        .Select(m => m.Content)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return users;
        }
    }
}
