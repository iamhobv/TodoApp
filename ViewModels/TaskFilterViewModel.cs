using static TodoApp.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using TodoApp.Models;

namespace TodoApp.ViewModels
{
    public class TaskFilterViewModel
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
        public string Content { get; set; }
        public DateTime DeadLine { get; set; }
        public Category Category { get; set; }
        public Enums.TaskStatus Status { get; set; }








        public string CreatedUserId { get; set; }
        public ApplicationUser? CreatedUser { get; set; }



        public string? AssignToUserId { get; set; }
        public ApplicationUser? AssignToUser { get; set; }
    }
}
