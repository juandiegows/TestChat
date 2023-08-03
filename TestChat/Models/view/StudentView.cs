using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestChat.Models.view {
    public class StudentView {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }
    }
}