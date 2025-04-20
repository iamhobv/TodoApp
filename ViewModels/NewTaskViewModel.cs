using System.ComponentModel;
using static TodoApp.Models.Enums;

namespace TodoApp.ViewModels
{
    public class NewTaskViewModel
    {
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Task Conetnt")]
        public string Content { get; set; }
        [DisplayName("Task Deadline Time")]

        public DateTime DeadLine { get; set; }
        [DisplayName("Task Category")]

        public Category Category { get; set; }
        public string CreatedUserId { get; set; }
        [DisplayName("Assign Task to someone else")]
        public string? AssignToUserId { get; set; }



    }
}
