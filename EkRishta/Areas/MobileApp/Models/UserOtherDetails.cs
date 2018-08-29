using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Areas.Models
{
    public class UserOtherDetails
    {
        public int UserOtherDetailsId { get; set; }
        public int BirthCountryId { get; set; }
        public string BirthCountryName { get; set; }
        public string BirthPlace { get; set; }
        public string BirthTime { get; set; }
        public string Height { get; set; }
        public string BodyType { get; set; }
        public string SkinTone { get; set; }
        public string BloodGroup { get; set; }
        public string IsSmoke { get; set; }
        public string IsDrink { get; set; }
        public string IsPhysicalDisable { get; set; }
        public string IdealpartnerDescription { get; set; }
        public int UserId { get; set; }

        public SelectList CountryDetails { get; set; }
    }
}