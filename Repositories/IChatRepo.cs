using TodoApp.ViewModels;

namespace TodoApp.Repositories
{
    public interface IChatRepo
    {
        Task<List<LastMsgViewModel>> GetUsers();
        Task<ChatViewModel> GetMsgs(string SelectedUserId);
        List<LastMsgViewModel> getChatsOnly();
    }
}
