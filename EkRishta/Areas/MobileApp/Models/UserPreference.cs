using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Areas.Models
{
    public class UserPreference
    {
        public int UserId { get; set; }

        public string FromAge { get; set; }
        public string ToAge { get; set; }
        public string FromHeight { get; set; }
        public string ToHeight { get; set; }
        public string MaritialStatus { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int UserReligionId { get; set; }
        public string ReligionId { get; set; }
        public string ReligionName { get; set; }
        public int CasteId { get; set; }
        public string CasteName { get; set; }
        public string MotherTounge { get; set; }
        public string MotherToungeId { get; set; }
        public string Income { get; set; }
        public string Diet { get; set; }
        public string IsDrink { get; set; }
        public string IsSmoke { get; set; }
        public string SkinTone { get; set; }
        public string BodyType { get; set; }
        public string IsPhysicalDisable { get; set; }
        public string Action { get; set; }

        public SelectList ReligionDetails { get; set; }
        public SelectList LanguageDetails { get; set; }
        public SelectList StateDetails { get; set; }
        public SelectList CityDetails { get; set; }
        //public SelectList CountryDetails { get; set; }
        public SelectList CasteDetails { get; set; }
    }
}