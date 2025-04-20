namespace TodoApp.ViewModels
{
    public class LastMsgViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string LastMsg { get; set; }
        public DateTime LastMsgDate { get; set; }

        public string? ProfilePicPath { get; set; }
    }
}
