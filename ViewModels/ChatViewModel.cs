namespace TodoApp.ViewModels
{
    public class ChatViewModel
    {
        public ChatViewModel()
        {
            Msgs = new List<MsgsInsideChat>();
        }
        public string CurrentUserId { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverUserName { get; set; }
        public string? ProfilePicPath { get; set; }

        public List<MsgsInsideChat> Msgs { get; set; }
    }
}
