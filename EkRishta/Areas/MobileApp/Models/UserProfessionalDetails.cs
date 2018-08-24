using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Areas.Models
{
    public class UserProfessionalDetails
    {
        public int UserId { get; set; }
        public string Degree { get; set; }
        public string Field { get; set; }
        public string CollegeName { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string Income { get; set; }
    }
}