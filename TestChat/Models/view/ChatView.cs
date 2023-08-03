using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestChat.Models.view {
    public class ChatView {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public long InstructorId { get; set; }
        public string Sender { get; set; }
        public System.DateTime Date { get; set; }
        public string Message { get; set; }
    }
}