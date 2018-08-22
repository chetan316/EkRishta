using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Areas.Models
{
    public class UserBasicDetails
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public SelectList DOBDayDetails { get; set; }
        public SelectList DOBMonthDetails { get; set; }
        public SelectList DOBYearDetails { get; set; }
        public string DOB { get; set; }
        public string DOBDay { get; set; }
        public string DOBMonth { get; set; }
        public string DOBYear { get; set; }
        public string UserAge { get; set; }
        public string UserGender { get; set; }
        public string UserMaritialStatus { get; set; }
        public string UserEmailId { get; set; }
        public string UserMobileNo { get; set; }
        public string UserProfileId { get; set; }
        public int UserId { get; set; }
    }
}