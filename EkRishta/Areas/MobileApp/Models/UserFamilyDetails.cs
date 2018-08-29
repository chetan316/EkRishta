using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Areas.Models
{
    public class UserFamilyDetails
    {
        public int UserFamilyDetailsId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string FatherProfession { get; set; }
        public string MotherProfession { get; set; }
        //public string FamilyLocation { get; set; }
        public string FamilyDescription { get; set; }
        public int UserId { get; set; }
    }
}