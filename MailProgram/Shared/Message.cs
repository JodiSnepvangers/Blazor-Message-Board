using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailProgram.Shared
{
    public class Message
    {   
        public int MessageId { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public User? User { get; set; }
        public int? UserId { get; set; }
    }
}
