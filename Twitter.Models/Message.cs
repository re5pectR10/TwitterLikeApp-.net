using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime SendOn { get; set; }
        public string SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public string ReceiverId { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
    }
}
