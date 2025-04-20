using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static TodoApp.Models.Enums;
using TaskStatus = TodoApp.Models.Enums.TaskStatus;

namespace TodoApp.Models
{
    public class MyTask
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
        public string Content { get; set; }
        public DateTime DeadLine { get; set; }
        public Category Category { get; set; }
        public TaskStatus Status { get; set; }







        [ForeignKey("CreatedUser")]
        public string CreatedUserId { get; set; }
        public ApplicationUser? CreatedUser { get; set; }



        [ForeignKey("AssignToUser")]
        public string? AssignToUserId { get; set; }
        public ApplicationUser? AssignToUser { get; set; }

    }
}
