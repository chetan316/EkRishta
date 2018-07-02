using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class User
    {
        public int UserId { get; set; }
        public string ProfileId { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
    }
}