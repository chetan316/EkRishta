using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Models
{
    public class UserRegistration
    {
        /// <summary>
        /// UserMaster
        /// </summary>
        public int UserId { get; set; }
        [Required (ErrorMessage="Please Enter First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Valid First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Valid First Name")]
        public string LastName { get; set; }
        public string ProfileId { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile No")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please Enter Only Numbers")]
        [StringLength(10,MinimumLength=10,ErrorMessage="Please Enter Valid Mobile No")]
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string EmailId { get; set; }
        public char IsSurnameVisible { get; set; }
        public char IsDPVisible { get; set; }
        public char IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string OTP { get; set; }
        public string ProfileCreatedBy { get; set; }
        public string CallTime { get; set; }
        public SelectList DOBDayDetails { get; set; }
        public SelectList DOBMonthDetails { get; set; }
        public SelectList DOBYearDetails { get; set; }
        public string DOBDay { get; set; }
        public string DOBMonth { get; set; }
        public string DOBYear { get; set; }

        /// <summary>
        /// UserAddressDetails
        /// </summary>
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string Pincode { get; set; }
        public string AlternateAddress1 { get; set; }
        public string AlternateAddress2 { get; set; }
        public int AlternateCityId { get; set; }
        public int AlternateStateId { get; set; }
        public int AlternateCountryId { get; set; }
        public string AlternatePincode { get; set; }
        /// <summary>
        /// UserReligionDetails
        /// </summary>
        public int UserReligionId { get; set; }
        public int ReligionId { get; set; }
        public int CastId { get; set; }
        public int SubCastId { get; set; }
        public string MoonSign { get; set; }
        public string Star { get; set; }
        public string Gotra { get; set; }
        /// <summary>
        /// UserFamilyDetails
        /// </summary>
        public int UserFamilyDetailsId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string FatherProfession { get; set; }
        public string MotherProfession { get; set; }
        public string FamilyLocation { get; set; }
        public string FamilyDescription { get; set; }
        /// <summary>
        /// UserOtherDetails
        /// </summary>
        public int UserOtherDetailsId { get; set; }
        public string MaritialStatus { get; set; }
        public string MotherTounge { get; set; }
        public string BirthCountry { get; set; }
        public string BirthPlace { get; set; }
        public string BirthTime { get; set; }
        public string Height { get; set; }
        public string BodyType { get; set; }
        public string SkinTone { get; set; }
        public string BloodGroup { get; set; }
        public char IsSmoke { get; set; }
        public char IsDrink { get; set; }
        public char IsPhysicalDisable { get; set; }
        public string IdealpartnerDescription { get; set; }
        /// <summary>
        /// UserProfessionalDetails
        /// </summary>
        public int UserProfessionalDetailsId { get; set; }
        public string CollegeName { get; set; }
        public string Degree { get; set; }
        public string Field { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string Income { get; set; }

        public SelectList ReligionDetails { get; set; }
        public SelectList LanguageDetails { get; set; }
        public SelectList StateDetails { get; set; }
        public SelectList CityDetails { get; set; }
    }
}