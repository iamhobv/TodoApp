using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace TodoApp.Models
{
    public class ApplicationUser : IdentityUser
    {

        [JsonIgnore]
        [InverseProperty("CreatedUser")]
        public List<MyTask>? MyTasks { get; set; }

        [JsonIgnore]
        [InverseProperty("AssignToUser")]
        public List<MyTask>? AssignedTasks { get; set; }



        [InverseProperty(nameof(Message.Sender))]
        public ICollection<Message> SendMessages { set; get; }

        [InverseProperty(nameof(Message.Receiver))]

        public ICollection<Message> ReceivedMessages { set; get; }



        public string ProfilePicUrl { get; set; }

    }
}
