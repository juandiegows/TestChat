using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestChat.Models.Request {
    public class StudentRequest {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}