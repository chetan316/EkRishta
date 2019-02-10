using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Areas.Models
{
    public class SearchUserProfile
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileId { get; set; }

        public string MobileNo { get; set; }

        public string DOB { get; set; }


        public string Age { get; set; }
        public string FromAge { get; set; }
        public string ToAge { get; set; }

        public string Gender { get; set; }

        public string EmailId { get; set; }

        public string IsSurnameVisible { get; set; }

        public string IsDPVisible { get; set; }

        public string IsActive { get; set; }
        public int ShareCount { get; set; }

        /// <summary>
        /// Address Table
        /// </summary>
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Pincode { get; set; }
        public string AlternateAddress1 { get; set; }
        public string AlternateAddress2 { get; set; }
        public int AlternateCityId { get; set; }
        public string AlternateCityName { get; set; }
        public int AlternateStateId { get; set; }
        public string AlternateStateName { get; set; }
        public int AlternateCountryId { get; set; }
        public string AlternateCountryName { get; set; }
        public string AlternatePincode { get; set; }

        /// <summary>
        /// Family Table
        /// </summary>
        public int UserFamilyDetailsId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string FatherProfession { get; set; }
        public string MotherProfession { get; set; }
        public string FamilyLocation { get; set; }
        public string FamilyDescription { get; set; }

        /// <summary>
        /// Other Details Table
        /// </summary>
        public int UserOtherDetailsId { get; set; }
        public string MaritialStatus { get; set; }
        public string MotherTounge { get; set; }
        public string MotherToungeId { get; set; }
        public string BirthCountry { get; set; }
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
        public string CallTime { get; set; }
        public string ProfileCreatedBy { get; set; }
        public string ProfilePicPath { get; set; }

        /// <summary>
        /// Religion Details Table
        /// </summary>
        public int UserReligionId { get; set; }
        public string ReligionId { get; set; }
        public string ReligionName { get; set; }
        public string CastId { get; set; }
        public string CastName { get; set; }
        public int SubCastId { get; set; }
        public string SubCastName { get; set; }
        public string MoonSign { get; set; }
        public string Star { get; set; }
        public string Gotra { get; set; }

        /// <summary>
        /// Professional Details Table
        /// </summary>
        public int UserProfessionalDetailsId { get; set; }
        public string CollegeName { get; set; }
        public string Degree { get; set; }
        public string Field { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string Income { get; set; }

        public string IsShortlisted { get; set; }

        public SelectList ReligionDetails { get; set; }
        public SelectList LanguageDetails { get; set; }
        public SelectList CityDetails { get; set; }
        public SelectList CastDetails { get; set; }
    }
}