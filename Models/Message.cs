using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;




        [ForeignKey("Sender")]

        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }






        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

    }
}
