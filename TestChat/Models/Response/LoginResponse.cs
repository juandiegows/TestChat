using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestChat.Models.view {
    public class LoginResponse {
        public String Token { get; set; }
        public StudentResponse User { get; set; }
        public DateTime DateTime { get; set; }
    }
}