using Models.Custom;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Areas.Models;

namespace EkRishta.Areas.MobileApp.Controllers
{
    [SessionAuthorize]
    public class UserController : BaseController
    {
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult MyProfile(int? UserId)
        {
            string response = string.Empty;
            DataSet dsResponse = new DataSet();
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                Models.User objUser = (Models.User)(Session["USER"]);
                if (UserId == null)
                    sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                else
                    sqlCmd.Parameters.AddWithValue("@UserId", UserId);

                sqlCmd.CommandText = "GetUserProfile";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                UserMaster objUserMaster = new UserMaster();
                UserBasicDetails objUserBasic = new UserBasicDetails();
                UserProfessionalDetails objUserProfessional = new UserProfessionalDetails();
                UserAddressDetails objUserAddress = new UserAddressDetails();
                UserFamilyDetails objUserFamily = new UserFamilyDetails();
                UserReligionDetails objUserReligion = new UserReligionDetails();
                UserOtherDetails objUserOther = new UserOtherDetails();

                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        objUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                        objUserMaster.LastName = Convert.ToString(dr["LastName"]);
                        objUserMaster.Gender = Convert.ToString(dr["Gender"]) == "M" ? "Male" : "Female";
                        objUserMaster.Age = Convert.ToString(dr["Age"]);
                        objUserMaster.DOB = Convert.ToString(dr["DOB"]);

                        objUserMaster.EmailId = Convert.ToString(dr["EmailId"]);
                        objUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);
                        objUserMaster.ProfileId = Convert.ToString(dr["ProfileId"]);

                        objUserMaster.Address1 = Convert.ToString(dr["Address1"]);
                        objUserMaster.Address2 = Convert.ToString(dr["Address2"]);
                        objUserMaster.CityName = Convert.ToString(dr["CityName"]);
                        objUserMaster.StateName = Convert.ToString(dr["StateName"]);
                        objUserMaster.CountryName = Convert.ToString(dr["CountryName"]);
                        objUserMaster.Pincode = Convert.ToString(dr["Pincode"]);
                        objUserMaster.AlternateAddress1 = Convert.ToString(dr["AlternateAddress1"]);
                        objUserMaster.AlternateAddress2 = Convert.ToString(dr["AlternateAddress2"]);
                        objUserMaster.AlternateCityName = "";//Convert.ToString(dr[""]);
                        objUserMaster.AlternateStateName = "";// Convert.ToString(dr[""]);
                        objUserMaster.AlternateCountryName = "";// Convert.ToString(dr[""]);
                        objUserMaster.AlternatePincode = Convert.ToString(dr["AlternatePincode"]);

                        objUserMaster.FatherName = Convert.ToString(dr["FatherName"]);
                        objUserMaster.FatherProfession = Convert.ToString(dr["FatherProfession"]);
                        objUserMaster.MotherName = Convert.ToString(dr["MotherName"]);
                        objUserMaster.MotherProfession = Convert.ToString(dr["MotherProfession"]);

                        objUserMaster.MaritialStatus = Convert.ToString(dr["MaritialStatus"]);
                        objUserMaster.MotherTounge = Convert.ToString(dr["MotherTounge"]);
                        objUserMaster.BirthCountry = Convert.ToString(dr["BirthCountryName"]);
                        objUserMaster.BirthPlace = Convert.ToString(dr["BirthPlace"]);
                        objUserMaster.BirthTime = Convert.ToString(dr["BirthTime"]);
                        objUserMaster.Height = Convert.ToString(dr["Height"]);
                        objUserMaster.BodyType = Convert.ToString(dr["BodyType"]);
                        objUserMaster.SkinTone = Convert.ToString(dr["SkinTone"]);
                        objUserMaster.BloodGroup = Convert.ToString(dr["BloodGroup"]);
                        objUserMaster.IsSmoke = Convert.ToString(dr["IsSmoke"]) == string.Empty ? "Not Specified" : Convert.ToString(dr["IsSmoke"]);
                        objUserMaster.IsDrink = Convert.ToString(dr["IsDrink"]) == string.Empty ? "Not Specified" : Convert.ToString(dr["IsDrink"]);
                        objUserMaster.IsPhysicalDisable = Convert.ToString(dr["IsPhysicalDisable"]) == string.Empty ? "Not Specified" : Convert.ToString(dr["IsPhysicalDisable"]);
                        objUserMaster.CallTime = Convert.ToString(dr["CallTime"]);
                        objUserMaster.ProfileCreatedBy = Convert.ToString(dr["ProfileCreatedBy"]);
                        objUserMaster.ProfilePicPath = "/Uploads/" + objUser.UserId + "/" + Convert.ToString(dr["ProfilePicPath"]);

                        objUserMaster.ReligionName = Convert.ToString(dr["ReligionName"]);
                        objUserMaster.CastName = Convert.ToString(dr["CastName"]);
                        //objUserMaster.SubCastName = "";//Convert.ToString(dr[""]);
                        objUserMaster.MoonSign = Convert.ToString(dr["MoonSign"]);
                        objUserMaster.Star = Convert.ToString(dr["Star"]);
                        objUserMaster.Gotra = Convert.ToString(dr["Gotra"]);

                        //objUserMaster.CollegeName = Convert.ToString(dr["CollegeName"]);
                        //objUserMaster.Field = Convert.ToString(dr["Field"]);
                        //objUserMaster.Degree = Convert.ToString(dr["Degree"]);
                        //objUserMaster.CompanyName = Convert.ToString(dr["CompanyName"]);
                        //objUserMaster.Designation = Convert.ToString(dr["Designation"]);
                        //objUserMaster.Income = Convert.ToString(dr["Income"]);

                        //UserBasicDetails
                        objUserBasic = BindBasicDetailsDropdown();
                        objUserBasic.DOB = Convert.ToString(dr["DOB"]);
                        objUserBasic.DOBDay = Convert.ToString(dr["DOB"]).Split('-')[0];
                        objUserBasic.DOBMonth = Convert.ToString(dr["DOB"]).Split('-')[1];
                        objUserBasic.DOBYear = Convert.ToString(dr["DOB"]).Split('-')[2];
                        objUserBasic.UserFirstName = Convert.ToString(dr["FirstName"]);
                        objUserBasic.UserLastName = Convert.ToString(dr["LastName"]);
                        objUserBasic.UserAge = Convert.ToString(dr["Age"]);
                        objUserBasic.UserGender = Convert.ToString(dr["Gender"]);
                        objUserBasic.UserMaritialStatus = Convert.ToString(dr["MaritialStatus"]);
                        objUserBasic.UserEmailId = Convert.ToString(dr["EmailId"]);
                        objUserBasic.UserMobileNo = Convert.ToString(dr["MobileNo"]);
                        objUserBasic.UserProfileId = Convert.ToString(dr["ProfileId"]);
                        objUserBasic.LanguageId = Convert.ToInt32(dr["MotherToungeId"]);
                        objUserBasic.MotherTounge = Convert.ToString(dr["MotherTounge"]);

                        objUserMaster.objUserBasicDetails = objUserBasic;

                        //Professional Details
                        objUserProfessional.Degree = Convert.ToString(dr["Degree"]);
                        objUserProfessional.Field = Convert.ToString(dr["Field"]);
                        objUserProfessional.CollegeName = Convert.ToString(dr["CollegeName"]);
                        objUserProfessional.CompanyName = Convert.ToString(dr["CompanyName"]);
                        objUserProfessional.Designation = Convert.ToString(dr["Designation"]);
                        objUserProfessional.Income = Convert.ToString(dr["Income"]);

                        objUserMaster.objUserProfessionalDetails = objUserProfessional;

                        //Address Details
                        objUserAddress = BindAddressDetailsDropdown();
                        objUserAddress.Address1 = Convert.ToString(dr["Address1"]);
                        objUserAddress.Address2 = Convert.ToString(dr["Address2"]);
                        objUserAddress.CityName = Convert.ToString(dr["CityName"]);
                        objUserAddress.CityId = Convert.ToInt32(dr["CityId"]);
                        objUserAddress.StateName = Convert.ToString(dr["StateName"]);
                        objUserAddress.CountryName = Convert.ToString(dr["CountryName"]);
                        objUserAddress.Pincode = Convert.ToString(dr["Pincode"]);
                        objUserAddress.AlternateAddress1 = Convert.ToString(dr["AlternateAddress1"]);
                        objUserAddress.AlternateAddress2 = Convert.ToString(dr["AlternateAddress2"]);
                        objUserAddress.AlternateCityName = "";//Convert.ToString(dr[""]);
                        objUserAddress.AlternateStateName = "";// Convert.ToString(dr[""]);
                        objUserAddress.AlternateCountryName = "";// Convert.ToString(dr[""]);
                        objUserAddress.AlternatePincode = Convert.ToString(dr["AlternatePincode"]);

                        objUserMaster.objUserAddressDetails = objUserAddress;

                        //Family Details
                        objUserFamily.FatherName = Convert.ToString(dr["FatherName"]);
                        objUserFamily.FatherProfession = Convert.ToString(dr["FatherProfession"]);
                        objUserFamily.MotherName = Convert.ToString(dr["MotherName"]);
                        objUserFamily.MotherProfession = Convert.ToString(dr["MotherProfession"]);
                        objUserFamily.FamilyDescription = Convert.ToString(dr["FamilyDescription"]);

                        objUserMaster.objUserFamilyDetails = objUserFamily;

                        //Religion Details
                        objUserReligion.CastDetails = new SelectList(CastDetails(), "Value", "Text");
                        objUserReligion.ReligionDetails = new SelectList(ReligionDetails(), "Value", "Text");
                        objUserReligion.ReligionId = Convert.ToInt32(dr["ReligionId"]);
                        objUserReligion.ReligionName = Convert.ToString(dr["ReligionName"]);
                        objUserReligion.CastId = Convert.ToInt32(dr["CastId"]);
                        objUserReligion.CastName = Convert.ToString(dr["CastName"]);
                        //objUserReligion.SubCastName = "";//Convert.ToString(dr[""]);
                        objUserReligion.MoonSign = Convert.ToString(dr["MoonSign"]);
                        objUserReligion.Star = Convert.ToString(dr["Star"]);
                        objUserReligion.Gotra = Convert.ToString(dr["Gotra"]);

                        objUserMaster.objUserReligionDetails = objUserReligion;

                        //Physical & Lifestyle Details(Other Details)
                        objUserOther.CountryDetails = new SelectList(CountryDetails(), "Value", "Text");
                        objUserOther.Height = Convert.ToString(dr["Height"]);
                        objUserOther.BodyType = Convert.ToString(dr["BodyType"]);
                        objUserOther.SkinTone = Convert.ToString(dr["SkinTone"]);
                        objUserOther.BloodGroup = Convert.ToString(dr["BloodGroup"]);
                        objUserOther.IsSmoke = Convert.ToString(dr["IsSmoke"]).Trim() == string.Empty ? "Not Specified" : Convert.ToString(dr["IsSmoke"]);
                        objUserOther.IsDrink = Convert.ToString(dr["IsDrink"]).Trim() == string.Empty ? "Not Specified" : Convert.ToString(dr["IsDrink"]);
                        objUserOther.IsPhysicalDisable = Convert.ToString(dr["IsPhysicalDisable"]).Trim() == string.Empty ? "Not Specified" : Convert.ToString(dr["IsPhysicalDisable"]);
                        objUserOther.BirthTime = Convert.ToString(dr["BirthTime"]);
                        objUserOther.BirthCountryName = Convert.ToString(dr["BirthCountryName"]);
                        objUserOther.BirthCountryId = Convert.ToInt32(dr["BirthCountryId"]);
                        objUserOther.BirthPlace = Convert.ToString(dr["BirthPlace"]);
                        objUserOther.IdealpartnerDescription = Convert.ToString(dr["IdealpartnerDescription"]);

                        objUserMaster.objUserOtherDetails = objUserOther;
                    }

                    objUserMaster.lstImages = new List<ImageUpload>();
                    if (dsResponse != null && dsResponse.Tables[1] != null)
                    {
                        foreach (DataRow dr in dsResponse.Tables[1].Rows)
                        {
                            ImageUpload objImageUpload = new ImageUpload();
                            objImageUpload.ImagePath = "/Uploads/" + objUser.UserId + "/" + Convert.ToString(dr["ImagePath"]);

                            objUserMaster.lstImages.Add(objImageUpload);
                        }
                    }
                }

                if (UserId == null)
                    return View("MyProfile", objUserMaster);
                else
                    return RedirectToAction("ViewProfile", objUserMaster);

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult MatchingProfile()
        {
            DataSet dsResponse = new DataSet();
            List<UserMaster> lstUserMaster = new List<UserMaster>();

            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                Models.User objUser = (Models.User)(Session["USER"]);
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                sqlCmd.Parameters.AddWithValue("@ReligionId", objUser.ReligionId);
                sqlCmd.CommandText = "GetMatchingProfile";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);


                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        UserMaster objUserMaster = new UserMaster();

                        objUserMaster.RequestSource = MethodInfo.GetCurrentMethod().Name;
                        objUserMaster.UserId = Convert.ToInt32(dr["UserId"]);
                        objUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                        objUserMaster.LastName = Convert.ToString(dr["LastName"]);
                        objUserMaster.ProfileId = Convert.ToString(dr["ProfileId"]);
                        objUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);
                        objUserMaster.DOB = Convert.ToString(dr["DOB"]);
                        objUserMaster.Age = Convert.ToString(dr["Age"]);
                        objUserMaster.Gender = Convert.ToString(dr["Gender"]) == "M" ? "Male" : "Female";
                        objUserMaster.EmailId = Convert.ToString(dr["EmailId"]);
                        objUserMaster.IsSurnameVisible = Convert.ToString(dr["IsSurnameVisible"]);
                        objUserMaster.IsDPVisible = Convert.ToString(dr["IsDPVisible"]);
                        objUserMaster.MaritialStatus = Convert.ToString(dr["MaritialStatus"]) == "1" ? "Unmarried" : "Married";
                        objUserMaster.Height = Convert.ToString(dr["Height"]);
                        objUserMaster.ProfilePicPath = "/Uploads/" + objUserMaster.UserId + "/" + Convert.ToString(dr["ProfilePicPath"]);
                        objUserMaster.ReligionId = Convert.ToInt32(dr["ReligionId"]);
                        objUserMaster.ReligionName = Convert.ToString(dr["ReligionName"]);
                        objUserMaster.CastId = Convert.ToInt32(dr["CastId"]);
                        objUserMaster.CastName = Convert.ToString(dr["CastName"]);
                        //objUserMaster.RequestStatus = Convert.ToString(dr["RequestStatus"]);
                        objUserMaster.ShareCount = objUser.ShareCount;

                        foreach (DataRow dr1 in dsResponse.Tables[1].Rows)
                        {
                            int UserId = Convert.ToInt32(dr1["ShortlistedUserId"]);
                            string shortlistedStatus = Convert.ToString(dr1["Status"]);
                            if (objUserMaster.UserId == UserId && shortlistedStatus == "S")
                                objUserMaster.IsShortlisted = "S";
                            else if (objUserMaster.UserId == UserId && shortlistedStatus == "NS")
                                objUserMaster.IsShortlisted = "NS";
                        }
                        lstUserMaster.Add(objUserMaster);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return View("RequestInfo", lstUserMaster);
        }

        public ActionResult SentRequest()
        {
            DataSet dsResponse = new DataSet();
            List<UserMaster> lstUserMaster = new List<UserMaster>();

            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                Models.User objUser = (Models.User)(Session["USER"]);
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                sqlCmd.Parameters.AddWithValue("@RequestStatus", "Pending");
                sqlCmd.CommandText = "GetRequestByStatus";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        UserMaster objUserMaster = new UserMaster();

                        objUserMaster.RequestSource = MethodInfo.GetCurrentMethod().Name;
                        objUserMaster.UserId = Convert.ToInt32(dr["UserId"]);
                        objUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                        objUserMaster.LastName = Convert.ToString(dr["LastName"]);
                        objUserMaster.ProfileId = Convert.ToString(dr["ProfileId"]);
                        objUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);
                        objUserMaster.DOB = Convert.ToString(dr["DOB"]);
                        objUserMaster.Age = Convert.ToString(dr["Age"]);
                        objUserMaster.Gender = Convert.ToString(dr["Gender"]) == "M" ? "Male" : "Female";
                        objUserMaster.EmailId = Convert.ToString(dr["EmailId"]);
                        objUserMaster.IsSurnameVisible = Convert.ToString(dr["IsSurnameVisible"]);
                        objUserMaster.IsDPVisible = Convert.ToString(dr["IsDPVisible"]);
                        objUserMaster.MaritialStatus = Convert.ToString(dr["MaritialStatus"]);
                        objUserMaster.Height = Convert.ToString(dr["Height"]);
                        objUserMaster.ProfilePicPath = "/Uploads/" + objUserMaster.UserId + "/" + Convert.ToString(dr["ProfilePicPath"]);
                        objUserMaster.ReligionId = Convert.ToInt32(dr["ReligionId"]);
                        objUserMaster.ReligionName = Convert.ToString(dr["ReligionName"]);
                        objUserMaster.CastId = Convert.ToInt32(dr["CastId"]);
                        objUserMaster.CastName = Convert.ToString(dr["CastName"]);
                        objUserMaster.ShareCount = objUser.ShareCount;

                        lstUserMaster.Add(objUserMaster);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return View("RequestInfo", lstUserMaster);
        }

        public ActionResult AcceptedRequest(string RequestType)
        {
            DataSet dsResponse = new DataSet();
            List<UserMaster> lstUserMaster = new List<UserMaster>();

            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                Models.User objUser = (Models.User)(Session["USER"]);
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                sqlCmd.Parameters.AddWithValue("@RequestType", RequestType);
                sqlCmd.CommandText = "GetAcceptedRequest";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        UserMaster objUserMaster = new UserMaster();

                        objUserMaster.RequestSource = MethodInfo.GetCurrentMethod().Name;
                        objUserMaster.UserId = Convert.ToInt32(dr["UserId"]);
                        objUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                        objUserMaster.LastName = Convert.ToString(dr["LastName"]);
                        objUserMaster.ProfileId = Convert.ToString(dr["ProfileId"]);
                        objUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);
                        objUserMaster.DOB = Convert.ToString(dr["DOB"]);
                        objUserMaster.Age = Convert.ToString(dr["Age"]);
                        objUserMaster.Gender = Convert.ToString(dr["Gender"]) == "M" ? "Male" : "Female";
                        objUserMaster.EmailId = Convert.ToString(dr["EmailId"]);
                        objUserMaster.IsSurnameVisible = Convert.ToString(dr["IsSurnameVisible"]);
                        objUserMaster.IsDPVisible = Convert.ToString(dr["IsDPVisible"]);
                        objUserMaster.MaritialStatus = Convert.ToString(dr["MaritialStatus"]);
                        objUserMaster.Height = Convert.ToString(dr["Height"]);
                        objUserMaster.ProfilePicPath = "/Uploads/" + objUserMaster.UserId + "/" + Convert.ToString(dr["ProfilePicPath"]);
                        objUserMaster.ReligionId = Convert.ToInt32(dr["ReligionId"]);
                        objUserMaster.ReligionName = Convert.ToString(dr["ReligionName"]);
                        objUserMaster.CastId = Convert.ToInt32(dr["CastId"]);
                        objUserMaster.CastName = Convert.ToString(dr["CastName"]);
                        objUserMaster.ShareCount = objUser.ShareCount;

                        lstUserMaster.Add(objUserMaster);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View("RequestInfo", lstUserMaster);
        }

        public ActionResult BlockedUser()
        {
            DataSet dsResponse = new DataSet();
            List<UserMaster> lstUserMaster = new List<UserMaster>();

            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                Models.User objUser = (Models.User)(Session["USER"]);
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                //sqlCmd.Parameters.AddWithValue("@RequestStatus", "Blocked");
                sqlCmd.CommandText = "GetBlockedRequest";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        UserMaster objUserMaster = new UserMaster();

                        objUserMaster.RequestSource = MethodInfo.GetCurrentMethod().Name;
                        objUserMaster.UserId = Convert.ToInt32(dr["UserId"]);
                        objUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                        objUserMaster.LastName = Convert.ToString(dr["LastName"]);
                        objUserMaster.ProfileId = Convert.ToString(dr["ProfileId"]);
                        objUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);
                        objUserMaster.DOB = Convert.ToString(dr["DOB"]);
                        objUserMaster.Age = Convert.ToString(dr["Age"]);
                        objUserMaster.Gender = Convert.ToString(dr["Gender"]) == "M" ? "Male" : "Female";
                        objUserMaster.EmailId = Convert.ToString(dr["EmailId"]);
                        objUserMaster.IsSurnameVisible = Convert.ToString(dr["IsSurnameVisible"]);
                        objUserMaster.IsDPVisible = Convert.ToString(dr["IsDPVisible"]);
                        objUserMaster.MaritialStatus = Convert.ToString(dr["MaritialStatus"]);
                        objUserMaster.Height = Convert.ToString(dr["Height"]);
                        objUserMaster.ProfilePicPath = "/Uploads/" + objUserMaster.UserId + "/" + Convert.ToString(dr["ProfilePicPath"]);
                        objUserMaster.ReligionId = Convert.ToInt32(dr["ReligionId"]);
                        objUserMaster.ReligionName = Convert.ToString(dr["ReligionName"]);
                        objUserMaster.CastId = Convert.ToInt32(dr["CastId"]);
                        objUserMaster.CastName = Convert.ToString(dr["CastName"]);
                        objUserMaster.ShareCount = objUser.ShareCount;

                        lstUserMaster.Add(objUserMaster);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View("RequestInfo", lstUserMaster);
        }

        public ActionResult RejectedRequest()
        {
            DataSet dsResponse = new DataSet();
            List<UserMaster> lstUserMaster = new List<UserMaster>();

            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                Models.User objUser = (Models.User)(Session["USER"]);
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                //sqlCmd.Parameters.AddWithValue("@RequestStatus", "Blocked");
                sqlCmd.CommandText = "GetRejectedRequest ";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        UserMaster objUserMaster = new UserMaster();

                        objUserMaster.RequestSource = MethodInfo.GetCurrentMethod().Name;
                        objUserMaster.UserId = Convert.ToInt32(dr["UserId"]);
                        objUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                        objUserMaster.LastName = Convert.ToString(dr["LastName"]);
                        objUserMaster.ProfileId = Convert.ToString(dr["ProfileId"]);
                        objUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);
                        objUserMaster.DOB = Convert.ToString(dr["DOB"]);
                        objUserMaster.Age = Convert.ToString(dr["Age"]);
                        objUserMaster.Gender = Convert.ToString(dr["Gender"]) == "M" ? "Male" : "Female";
                        objUserMaster.EmailId = Convert.ToString(dr["EmailId"]);
                        objUserMaster.IsSurnameVisible = Convert.ToString(dr["IsSurnameVisible"]);
                        objUserMaster.IsDPVisible = Convert.ToString(dr["IsDPVisible"]);
                        objUserMaster.MaritialStatus = Convert.ToString(dr["MaritialStatus"]);
                        objUserMaster.Height = Convert.ToString(dr["Height"]);
                        objUserMaster.ProfilePicPath = "/Uploads/" + objUserMaster.UserId + "/" + Convert.ToString(dr["ProfilePicPath"]);
                        objUserMaster.ReligionId = Convert.ToInt32(dr["ReligionId"]);
                        objUserMaster.ReligionName = Convert.ToString(dr["ReligionName"]);
                        objUserMaster.CastId = Convert.ToInt32(dr["CastId"]);
                        objUserMaster.CastName = Convert.ToString(dr["CastName"]);
                        objUserMaster.ShareCount = objUser.ShareCount;

                        lstUserMaster.Add(objUserMaster);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View("RequestInfo", lstUserMaster);
        }

        public ActionResult ProfileRequest()
        {
            DataSet dsResponse = new DataSet();
            List<UserMaster> lstUserMaster = new List<UserMaster>();

            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                Models.User objUser = (Models.User)(Session["USER"]);
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                sqlCmd.CommandText = "GetProfileRequest";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);


                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        UserMaster objUserMaster = new UserMaster();

                        objUserMaster.UserId = Convert.ToInt32(dr["RequestingUserId"]);
                        objUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                        objUserMaster.LastName = Convert.ToString(dr["LastName"]);
                        objUserMaster.ProfileId = Convert.ToString(dr["ProfileId"]);
                        objUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);
                        objUserMaster.DOB = Convert.ToString(dr["DOB"]);
                        objUserMaster.Age = Convert.ToString(dr["Age"]);
                        objUserMaster.Gender = Convert.ToString(dr["Gender"]) == "M" ? "Male" : "Female";
                        objUserMaster.EmailId = Convert.ToString(dr["EmailId"]);
                        objUserMaster.IsSurnameVisible = Convert.ToString(dr["IsSurnameVisible"]);
                        objUserMaster.IsDPVisible = Convert.ToString(dr["IsDPVisible"]);
                        objUserMaster.MaritialStatus = Convert.ToString(dr["MaritialStatus"]) == "1" ? "Unmarried" : "Married";
                        objUserMaster.Height = Convert.ToString(dr["Height"]);
                        objUserMaster.ProfilePicPath = "/Uploads/" + objUserMaster.UserId + "/" + Convert.ToString(dr["ProfilePicPath"]);
                        objUserMaster.ReligionId = Convert.ToInt32(dr["ReligionId"]);
                        objUserMaster.ReligionName = Convert.ToString(dr["ReligionName"]);
                        objUserMaster.CastId = Convert.ToInt32(dr["CastId"]);
                        objUserMaster.CastName = Convert.ToString(dr["CastName"]);
                        objUserMaster.RequestStatus = Convert.ToString(dr["RequestStatus"]);
                        objUserMaster.ShareCount = objUser.ShareCount;

                        lstUserMaster.Add(objUserMaster);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return View("ProfileRequest", lstUserMaster);
        }

        [HttpPost]
        public ActionResult SendRequest(ManageRequest objManageRequest)
        {
            DataSet dsResponse = new DataSet();
            string jsonResponse = string.Empty;
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                Models.User objUser = (Models.User)(Session["USER"]);
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                sqlCmd.Parameters.AddWithValue("@RequestedUserId", objManageRequest.RequestedUserId);
                sqlCmd.Parameters.AddWithValue("@Status", objManageRequest.RequestStatus);
                sqlCmd.CommandText = "ManageSendRequest";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);
                if (dsResponse != null && dsResponse.Tables[0] != null && dsResponse.Tables[0].Rows.Count > 0)
                {
                    jsonResponse = Convert.ToString(dsResponse.Tables[0].Rows[0]["Result"]);
                }
            }
            catch (Exception ex)
            {

            }
            //return View();
            return Json(jsonResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AccceptRejectRequest(ManageRequest objManageRequest)
        {
            DataSet dsResponse = new DataSet();
            string jsonResponse = string.Empty;
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                Models.User objUser = (Models.User)(Session["USER"]);
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                sqlCmd.Parameters.AddWithValue("@RequestedUserId", objManageRequest.RequestedUserId);
                sqlCmd.Parameters.AddWithValue("@Status", objManageRequest.RequestStatus);
                sqlCmd.CommandText = "ManageSendRequest";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);
                if (dsResponse != null && dsResponse.Tables[0] != null && dsResponse.Tables[0].Rows.Count > 0)
                {
                    jsonResponse = Convert.ToString(dsResponse.Tables[0].Rows[0]["Result"]);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(jsonResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewProfile(int UserId)
        {
            string response = string.Empty;
            DataSet dsResponse = new DataSet();
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@UserId", UserId);

                sqlCmd.CommandText = "GetUserProfile";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                Models.User objUser = (Models.User)(Session["USER"]);
                UserMaster objUserMaster = new UserMaster();

                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    ManageProfileVisitors(UserId, "I");
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        objUserMaster.ShareCount = objUser.ShareCount;

                        objUserMaster.UserId = Convert.ToInt32(dr["UserId"]);
                        objUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                        objUserMaster.LastName = Convert.ToString(dr["LastName"]);
                        objUserMaster.Gender = Convert.ToString(dr["Gender"]) == "M" ? "Male" : "Female";
                        objUserMaster.Age = Convert.ToString(dr["Age"]);
                        objUserMaster.DOB = Convert.ToString(dr["DOB"]);
                        objUserMaster.EmailId = Convert.ToString(dr["EmailId"]);
                        objUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);
                        objUserMaster.ProfileId = Convert.ToString(dr["ProfileId"]);

                        objUserMaster.Address1 = Convert.ToString(dr["Address1"]);
                        objUserMaster.Address2 = Convert.ToString(dr["Address2"]);
                        objUserMaster.CityName = Convert.ToString(dr["CityName"]);
                        objUserMaster.StateName = Convert.ToString(dr["StateName"]);
                        objUserMaster.CountryName = Convert.ToString(dr["CountryName"]);
                        objUserMaster.Pincode = Convert.ToString(dr["Pincode"]);
                        objUserMaster.AlternateAddress1 = Convert.ToString(dr["AlternateAddress1"]);
                        objUserMaster.AlternateAddress2 = Convert.ToString(dr["AlternateAddress2"]);
                        objUserMaster.AlternateCityName = "";//Convert.ToString(dr[""]);
                        objUserMaster.AlternateStateName = "";// Convert.ToString(dr[""]);
                        objUserMaster.AlternateCountryName = "";// Convert.ToString(dr[""]);
                        objUserMaster.AlternatePincode = Convert.ToString(dr["AlternatePincode"]);

                        objUserMaster.FatherName = Convert.ToString(dr["FatherName"]);
                        objUserMaster.FatherProfession = Convert.ToString(dr["FatherProfession"]);
                        objUserMaster.MotherName = Convert.ToString(dr["MotherName"]);
                        objUserMaster.MotherProfession = Convert.ToString(dr["MotherProfession"]);

                        objUserMaster.MaritialStatus = Convert.ToString(dr["MaritialStatus"]);
                        objUserMaster.MotherTounge = Convert.ToString(dr["MotherTounge"]);
                        objUserMaster.BirthCountry = Convert.ToString(dr["BirthCountryName"]);
                        objUserMaster.BirthPlace = Convert.ToString(dr["BirthPlace"]);
                        objUserMaster.BirthTime = Convert.ToString(dr["BirthTime"]);
                        objUserMaster.Height = Convert.ToString(dr["Height"]);
                        objUserMaster.BodyType = Convert.ToString(dr["BodyType"]);
                        objUserMaster.SkinTone = Convert.ToString(dr["SkinTone"]);
                        objUserMaster.BloodGroup = Convert.ToString(dr["BloodGroup"]);
                        objUserMaster.IsSmoke = Convert.ToString(dr["IsSmoke"]);
                        objUserMaster.IsDrink = Convert.ToString(dr["IsDrink"]);
                        objUserMaster.IsPhysicalDisable = Convert.ToString(dr["IsPhysicalDisable"]);
                        objUserMaster.CallTime = Convert.ToString(dr["CallTime"]);
                        objUserMaster.ProfileCreatedBy = Convert.ToString(dr["ProfileCreatedBy"]);
                        objUserMaster.ProfilePicPath = "/Uploads/" + UserId + "/" + Convert.ToString(dr["ProfilePicPath"]);

                        objUserMaster.ReligionName = Convert.ToString(dr["ReligionName"]);
                        objUserMaster.CastName = Convert.ToString(dr["CastName"]);
                        //objUserMaster.SubCastName = "";//Convert.ToString(dr[""]);
                        objUserMaster.MoonSign = Convert.ToString(dr["MoonSign"]);
                        objUserMaster.Star = Convert.ToString(dr["Star"]);
                        objUserMaster.Gotra = Convert.ToString(dr["Gotra"]);

                        objUserMaster.CollegeName = Convert.ToString(dr["CollegeName"]);
                        objUserMaster.Field = Convert.ToString(dr["Field"]);
                        objUserMaster.Degree = Convert.ToString(dr["Degree"]);
                        objUserMaster.CompanyName = Convert.ToString(dr["CompanyName"]);
                        objUserMaster.Designation = Convert.ToString(dr["Designation"]);
                        objUserMaster.Income = Convert.ToString(dr["Income"]);
                    }
                    objUserMaster.lstImages = new List<ImageUpload>();
                    if (dsResponse != null && dsResponse.Tables[1] != null)
                    {
                        foreach (DataRow dr in dsResponse.Tables[1].Rows)
                        {
                            ImageUpload objImageUpload = new ImageUpload();
                            objImageUpload.ImagePath = "/Uploads/" + objUserMaster.UserId + "/" + Convert.ToString(dr["ImagePath"]);

                            objUserMaster.lstImages.Add(objImageUpload);
                        }
                    }
                }

                return View("ViewProfile", objUserMaster);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult _BasicDetails()
        {
            UserBasicDetails objUserBasic = BindBasicDetailsDropdown();
            return View(objUserBasic);
        }
        public ActionResult _AddressDetails()
        {
            UserAddressDetails objUserAddress = BindAddressDetailsDropdown();
            return View(objUserAddress);
        }
        public ActionResult _ReligionDetails()
        {
            UserReligionDetails objUserReligion = new UserReligionDetails();
            objUserReligion.ReligionDetails = new SelectList(ReligionDetails(), "Value", "Text");
            objUserReligion.CastDetails = new SelectList(CastDetails(), "Value", "Text");
            return View(objUserReligion);
        }
        public ActionResult _OtherDetails()
        {
            UserOtherDetails objUserOther = new UserOtherDetails();
            objUserOther.CountryDetails = new SelectList(CountryDetails(), "Value", "Text");
            return View(objUserOther);
        }

        #region Update User Details

        [HttpPost]
        public ActionResult UpdateBasicDetails(UserBasicDetails objUserBasicDetails)
        {
            DataSet dsResponse = new DataSet();
            try
            {
                Models.User objUser = (Models.User)(Session["USER"]);

                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                sqlCmd.Parameters.AddWithValue("@FirstName", objUserBasicDetails.UserFirstName);
                sqlCmd.Parameters.AddWithValue("@LastName", objUserBasicDetails.UserLastName);
                sqlCmd.Parameters.AddWithValue("@DOB", objUserBasicDetails.DOBDay.ToString() + "-" + objUserBasicDetails.DOBMonth.ToString() + "-" + objUserBasicDetails.DOBYear.ToString());
                sqlCmd.Parameters.AddWithValue("@Age", objUserBasicDetails.UserAge);
                sqlCmd.Parameters.AddWithValue("@Gender", objUserBasicDetails.UserGender);
                sqlCmd.Parameters.AddWithValue("@EmailId", objUserBasicDetails.UserEmailId);
                sqlCmd.Parameters.AddWithValue("@MaritialStatus", objUserBasicDetails.UserMaritialStatus);
                sqlCmd.Parameters.AddWithValue("@MotherTounge", objUserBasicDetails.LanguageId);

                sqlCmd.CommandText = "UpdateBasicDetails";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                UserBasicDetails objBasicDetails = new UserBasicDetails();
                objBasicDetails = BindBasicDetailsDropdown();
                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        objBasicDetails.UserFirstName = Convert.ToString(dr["FirstName"]);
                        objBasicDetails.UserLastName = Convert.ToString(dr["LastName"]);
                        objBasicDetails.UserGender = Convert.ToString(dr["Gender"]);
                        objBasicDetails.UserAge = Convert.ToString(dr["Age"]);
                        objBasicDetails.DOB = Convert.ToString(dr["DOB"]);
                        objBasicDetails.DOBDay = Convert.ToString(dr["DOB"]).Split('-')[0];
                        objBasicDetails.DOBMonth = Convert.ToString(dr["DOB"]).Split('-')[1];
                        objBasicDetails.DOBYear = Convert.ToString(dr["DOB"]).Split('-')[2];
                        objBasicDetails.UserEmailId = Convert.ToString(dr["EmailId"]);
                        objBasicDetails.UserMobileNo = Convert.ToString(dr["MobileNo"]);
                        objBasicDetails.UserProfileId = Convert.ToString(dr["ProfileId"]);
                        objBasicDetails.UserMaritialStatus = Convert.ToString(dr["MaritialStatus"]);
                        objBasicDetails.LanguageId = Convert.ToInt32(dr["MotherTounge"]);
                        objBasicDetails.MotherTounge = Convert.ToString(dr["LanguageName"]);
                    }
                }

                return PartialView("~/Areas/MobileApp/Views/User/_BasicDetails.cshtml", objBasicDetails);
            }
            catch (Exception ex)
            {

                throw;
                return null;
            }
        }

        [HttpPost]
        public ActionResult UpdateProfessionalDetails(UserProfessionalDetails objUserProfessionalDetails)
        {
            DataSet dsResponse = new DataSet();
            try
            {
                Models.User objUser = (Models.User)(Session["USER"]);

                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                sqlCmd.Parameters.AddWithValue("@Degree", objUserProfessionalDetails.Degree);
                sqlCmd.Parameters.AddWithValue("@Field", objUserProfessionalDetails.Field);
                sqlCmd.Parameters.AddWithValue("@CollegeName", objUserProfessionalDetails.CollegeName);
                sqlCmd.Parameters.AddWithValue("@CompanyName", objUserProfessionalDetails.CompanyName);
                sqlCmd.Parameters.AddWithValue("@Designation", objUserProfessionalDetails.Designation);
                sqlCmd.Parameters.AddWithValue("@Income", objUserProfessionalDetails.Income);

                sqlCmd.CommandText = "UpdateProfessionalDetails";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                UserProfessionalDetails objProfessionalDetails = new UserProfessionalDetails();
                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        objProfessionalDetails.Degree = Convert.ToString(dr["Degree"]);
                        objProfessionalDetails.Field = Convert.ToString(dr["Field"]);
                        objProfessionalDetails.CollegeName = Convert.ToString(dr["CollegeName"]);
                        objProfessionalDetails.CompanyName = Convert.ToString(dr["CompanyName"]);
                        objProfessionalDetails.Designation = Convert.ToString(dr["Designation"]);
                        objProfessionalDetails.Income = Convert.ToString(dr["Income"]);
                    }
                }

                return PartialView("~/Areas/MobileApp/Views/User/_ProfessionalDetails.cshtml", objProfessionalDetails);
            }
            catch (Exception ex)
            {

                throw;
                return null;
            }
        }

        [HttpPost]
        public ActionResult UpdateAddressDetails(UserAddressDetails objUserAddressDetails)
        {
            DataSet dsResponse = new DataSet();
            try
            {
                Models.User objUser = (Models.User)(Session["USER"]);
                objUserAddressDetails.UserId = objUser.UserId;

                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@UserId", objUserAddressDetails.UserId);
                sqlCmd.Parameters.AddWithValue("@Address1", objUserAddressDetails.Address1);
                sqlCmd.Parameters.AddWithValue("@Address2", objUserAddressDetails.Address2);
                sqlCmd.Parameters.AddWithValue("@CityId", objUserAddressDetails.CityId);
                sqlCmd.Parameters.AddWithValue("@StateId", objUserAddressDetails.StateId);
                sqlCmd.Parameters.AddWithValue("@CountryId", objUserAddressDetails.CountryId);
                sqlCmd.Parameters.AddWithValue("@Pincode", objUserAddressDetails.Pincode);

                sqlCmd.CommandText = "UpdateAddressDetails";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                UserAddressDetails objAddressDetails = new UserAddressDetails();
                objAddressDetails = BindAddressDetailsDropdown();
                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        objAddressDetails.Address1 = Convert.ToString(dr["Address1"]);
                        objAddressDetails.Address2 = Convert.ToString(dr["Address2"]);
                        objAddressDetails.CityId = Convert.ToInt32(dr["CityId"]);
                        objAddressDetails.CityName = Convert.ToString(dr["CityName"]);
                        objAddressDetails.StateId = Convert.ToInt32(dr["StateId"]);
                        objAddressDetails.StateName = Convert.ToString(dr["StateName"]);
                        objAddressDetails.CountryId = Convert.ToInt32(dr["CountryId"]);
                        objAddressDetails.CountryName = Convert.ToString(dr["CountryName"]);
                        objAddressDetails.Pincode = Convert.ToString(dr["Pincode"]);
                    }
                }

                return PartialView("~/Areas/MobileApp/Views/User/_AddressDetails.cshtml", objAddressDetails);
            }
            catch (Exception ex)
            {

                throw;
                return null;
            }
        }

        [HttpPost]
        public ActionResult UpdateFamilyDetails(UserFamilyDetails objUserFamilyDetails)
        {
            DataSet dsResponse = new DataSet();
            try
            {
                Models.User objUser = (Models.User)(Session["USER"]);
                objUserFamilyDetails.UserId = objUser.UserId;

                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@UserId", objUserFamilyDetails.UserId);
                sqlCmd.Parameters.AddWithValue("@FatherName", objUserFamilyDetails.FatherName);
                sqlCmd.Parameters.AddWithValue("@FatherProfession", objUserFamilyDetails.FatherProfession);
                sqlCmd.Parameters.AddWithValue("@MotherName", objUserFamilyDetails.MotherName);
                sqlCmd.Parameters.AddWithValue("@MotherProfession", objUserFamilyDetails.MotherProfession);
                sqlCmd.Parameters.AddWithValue("@FamilyDescription", objUserFamilyDetails.FamilyDescription);

                sqlCmd.CommandText = "UpdateFamilyDetails";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                UserFamilyDetails objFamilyDetails = new UserFamilyDetails();
                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        objFamilyDetails.FatherName = Convert.ToString(dr["FatherName"]);
                        objFamilyDetails.FatherProfession = Convert.ToString(dr["FatherProfession"]);
                        objFamilyDetails.MotherName = Convert.ToString(dr["MotherName"]);
                        objFamilyDetails.MotherProfession = Convert.ToString(dr["MotherProfession"]);
                        objFamilyDetails.FamilyDescription = Convert.ToString(dr["FamilyDescription"]);
                    }
                }

                return PartialView("~/Areas/MobileApp/Views/User/_FamilyDetails.cshtml", objFamilyDetails);
            }
            catch (Exception ex)
            {

                throw;
                return null;
            }
        }

        [HttpPost]
        public ActionResult UpdateReligionDetails(UserReligionDetails objUserReligionDetails)
        {
            DataSet dsResponse = new DataSet();
            try
            {
                Models.User objUser = (Models.User)(Session["USER"]);
                objUserReligionDetails.UserId = objUser.UserId;

                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@UserId", objUserReligionDetails.UserId);
                sqlCmd.Parameters.AddWithValue("@ReligionId", objUserReligionDetails.ReligionId);
                sqlCmd.Parameters.AddWithValue("@CastId", objUserReligionDetails.CastId);
                sqlCmd.Parameters.AddWithValue("@MoonSign", objUserReligionDetails.MoonSign);
                sqlCmd.Parameters.AddWithValue("@Star", objUserReligionDetails.Star);
                sqlCmd.Parameters.AddWithValue("@Gotra", objUserReligionDetails.Gotra);

                sqlCmd.CommandText = "UpdateReligionDetails";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                UserReligionDetails objReligionDetails = new UserReligionDetails();
                objReligionDetails.ReligionDetails = new SelectList(ReligionDetails(), "Value", "Text");
                objReligionDetails.CastDetails = new SelectList(CastDetails(), "Value", "Text");
                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        objReligionDetails.ReligionId = Convert.ToInt32(dr["ReligionId"]);
                        objReligionDetails.ReligionName = Convert.ToString(dr["ReligionName"]);
                        objReligionDetails.CastId = Convert.ToInt32(dr["CastId"]);
                        objReligionDetails.CastName = Convert.ToString(dr["CastName"]);
                        //objReligionDetails.SubCastName = "";//Convert.ToString(dr[""]);
                        objReligionDetails.MoonSign = Convert.ToString(dr["MoonSign"]);
                        objReligionDetails.Star = Convert.ToString(dr["Star"]);
                        objReligionDetails.Gotra = Convert.ToString(dr["Gotra"]);
                    }
                }

                return PartialView("~/Areas/MobileApp/Views/User/_ReligionDetails.cshtml", objReligionDetails);
            }
            catch (Exception ex)
            {

                throw;
                return null;
            }
        }

        [HttpPost]
        public ActionResult UpdateOtherDetails(UserOtherDetails objUserOtherDetails)
        {
            DataSet dsResponse = new DataSet();
            try
            {
                Models.User objUser = (Models.User)(Session["USER"]);
                objUserOtherDetails.UserId = objUser.UserId;

                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@UserId", objUserOtherDetails.UserId);
                sqlCmd.Parameters.AddWithValue("@Height", objUserOtherDetails.Height);
                sqlCmd.Parameters.AddWithValue("@BodyType", objUserOtherDetails.BodyType);
                sqlCmd.Parameters.AddWithValue("@SkinTone", objUserOtherDetails.SkinTone);
                sqlCmd.Parameters.AddWithValue("@BloodGroup", objUserOtherDetails.BloodGroup);
                sqlCmd.Parameters.AddWithValue("@IsSmoke", objUserOtherDetails.IsSmoke);
                sqlCmd.Parameters.AddWithValue("@IsDrink", objUserOtherDetails.IsDrink);
                sqlCmd.Parameters.AddWithValue("@IsPhysicalDisable", objUserOtherDetails.IsPhysicalDisable);
                sqlCmd.Parameters.AddWithValue("@BirthPlace", objUserOtherDetails.BirthPlace);
                sqlCmd.Parameters.AddWithValue("@BirthTime", objUserOtherDetails.BirthTime);
                sqlCmd.Parameters.AddWithValue("@BirthCountryId", objUserOtherDetails.BirthCountryId);

                sqlCmd.CommandText = "UpdateOtherDetails";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                UserOtherDetails objOtherDetails = new UserOtherDetails();
                objOtherDetails.CountryDetails = new SelectList(CountryDetails(), "Value", "Text");
                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        objOtherDetails.Height = Convert.ToString(dr["Height"]);
                        objOtherDetails.BodyType = Convert.ToString(dr["BodyType"]);
                        objOtherDetails.SkinTone = Convert.ToString(dr["SkinTone"]);
                        objOtherDetails.BloodGroup = Convert.ToString(dr["BloodGroup"]);
                        objOtherDetails.IsSmoke = Convert.ToString(dr["IsSmoke"]);
                        objOtherDetails.IsDrink = Convert.ToString(dr["IsDrink"]);
                        objOtherDetails.IsPhysicalDisable = Convert.ToString(dr["IsPhysicalDisable"]);
                        objOtherDetails.BirthTime = Convert.ToString(dr["BirthTime"]);
                        objOtherDetails.BirthCountryName = Convert.ToString(dr["BirthCountryName"]);
                        objOtherDetails.BirthCountryId = Convert.ToInt32(dr["BirthCountryId"]);
                        objOtherDetails.BirthPlace = Convert.ToString(dr["BirthPlace"]);
                    }
                }

                return PartialView("~/Areas/MobileApp/Views/User/_OtherDetails.cshtml", objOtherDetails);
            }
            catch (Exception ex)
            {

                throw;
                return null;
            }
        }

        public UserBasicDetails BindBasicDetailsDropdown()
        {
            UserBasicDetails objUserBasic = new UserBasicDetails();
            try
            {
                objUserBasic.DOBDayDetails = new SelectList(DOBDayDetails(), "Value", "Text");
                objUserBasic.DOBMonthDetails = new SelectList(DOBMonthDetails(), "Value", "Text");
                objUserBasic.DOBYearDetails = new SelectList(DOBYearDetails(), "Value", "Text");
                objUserBasic.LanguageDetails = new SelectList(LanguageDetails(), "Value", "Text");
            }
            catch (Exception ex)
            {

                throw;
            }
            return objUserBasic;
        }
        public UserAddressDetails BindAddressDetailsDropdown()
        {
            UserAddressDetails objUserAddress = new UserAddressDetails();
            try
            {
                objUserAddress.CityDetails = new SelectList(CityDetails(), "Value", "Text");
                objUserAddress.StateDetails = new SelectList(StateDetails(), "Value", "Text");
                objUserAddress.CountryDetails = new SelectList(CountryDetails(), "Value", "Text");
            }
            catch (Exception ex)
            {

                throw;
            }
            return objUserAddress;
        }

        #endregion

        #region Shortlist Profile
        [HttpPost]
        public ActionResult ManageShortlistedProfile(ManageRequest objManageRequest)
        {
            DataSet dsResponse = new DataSet();
            string jsonResponse = string.Empty;
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                Models.User objUser = (Models.User)(Session["USER"]);
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                sqlCmd.Parameters.AddWithValue("@ShortlistedUserId", objManageRequest.RequestedUserId);
                sqlCmd.Parameters.AddWithValue("@Status", objManageRequest.RequestStatus);
                sqlCmd.CommandText = "ManageShortlistedProfiles";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);
                if (dsResponse != null && dsResponse.Tables[0] != null && dsResponse.Tables[0].Rows.Count > 0)
                {
                    jsonResponse = Convert.ToString(dsResponse.Tables[0].Rows[0]["Result"]);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(jsonResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShortlistedProfile()
        {
            string response = string.Empty;
            DataSet dsResponse = new DataSet();
            try
            {
                Models.User objUser = (Models.User)Session["USER"];
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);

                sqlCmd.CommandText = "GetShortlistedProfile";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                List<UserMaster> lstUserMaster = new List<UserMaster>();

                if (dsResponse != null && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        UserMaster objUserMaster = new UserMaster();

                        objUserMaster.UserId = Convert.ToInt32(dr["UserId"]);
                        objUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                        objUserMaster.LastName = Convert.ToString(dr["LastName"]);
                        objUserMaster.Gender = Convert.ToString(dr["Gender"]) == "M" ? "Male" : "Female";
                        objUserMaster.Age = Convert.ToString(dr["Age"]);
                        objUserMaster.DOB = Convert.ToString(dr["DOB"]);
                        objUserMaster.EmailId = Convert.ToString(dr["EmailId"]);
                        objUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);
                        objUserMaster.ProfileId = Convert.ToString(dr["ProfileId"]);

                        objUserMaster.Address1 = Convert.ToString(dr["Address1"]);
                        objUserMaster.Address2 = Convert.ToString(dr["Address2"]);
                        objUserMaster.CityName = Convert.ToString(dr["CityName"]);
                        objUserMaster.StateName = Convert.ToString(dr["StateName"]);
                        objUserMaster.CountryName = Convert.ToString(dr["CountryName"]);
                        objUserMaster.Pincode = Convert.ToString(dr["Pincode"]);
                        objUserMaster.AlternateAddress1 = Convert.ToString(dr["AlternateAddress1"]);
                        objUserMaster.AlternateAddress2 = Convert.ToString(dr["AlternateAddress2"]);
                        objUserMaster.AlternateCityName = "";//Convert.ToString(dr[""]);
                        objUserMaster.AlternateStateName = "";// Convert.ToString(dr[""]);
                        objUserMaster.AlternateCountryName = "";// Convert.ToString(dr[""]);
                        objUserMaster.AlternatePincode = Convert.ToString(dr["AlternatePincode"]);

                        objUserMaster.FatherName = Convert.ToString(dr["FatherName"]);
                        objUserMaster.FatherProfession = Convert.ToString(dr["FatherProfession"]);
                        objUserMaster.MotherName = Convert.ToString(dr["MotherName"]);
                        objUserMaster.MotherProfession = Convert.ToString(dr["MotherProfession"]);

                        objUserMaster.MaritialStatus = Convert.ToString(dr["MaritialStatus"]);
                        objUserMaster.MotherTounge = Convert.ToString(dr["MotherTounge"]);
                        objUserMaster.BirthCountry = Convert.ToString(dr["BirthCountryName"]);
                        objUserMaster.BirthPlace = Convert.ToString(dr["BirthPlace"]);
                        objUserMaster.BirthTime = Convert.ToString(dr["BirthTime"]);
                        objUserMaster.Height = Convert.ToString(dr["Height"]);
                        objUserMaster.BodyType = Convert.ToString(dr["BodyType"]);
                        objUserMaster.SkinTone = Convert.ToString(dr["SkinTone"]);
                        objUserMaster.BloodGroup = Convert.ToString(dr["BloodGroup"]);
                        objUserMaster.IsSmoke = Convert.ToString(dr["IsSmoke"]);
                        objUserMaster.IsDrink = Convert.ToString(dr["IsDrink"]);
                        objUserMaster.IsPhysicalDisable = Convert.ToString(dr["IsPhysicalDisable"]);
                        objUserMaster.CallTime = Convert.ToString(dr["CallTime"]);
                        objUserMaster.ProfileCreatedBy = Convert.ToString(dr["ProfileCreatedBy"]);
                        objUserMaster.ProfilePicPath = "/Uploads/" + objUserMaster.UserId + "/" + Convert.ToString(dr["ProfilePicPath"]);

                        objUserMaster.ReligionName = Convert.ToString(dr["ReligionName"]);
                        objUserMaster.CastName = Convert.ToString(dr["CastName"]);
                        //objUserMaster.SubCastName = "";//Convert.ToString(dr[""]);
                        objUserMaster.MoonSign = Convert.ToString(dr["MoonSign"]);
                        objUserMaster.Star = Convert.ToString(dr["Star"]);
                        objUserMaster.Gotra = Convert.ToString(dr["Gotra"]);

                        objUserMaster.CollegeName = Convert.ToString(dr["CollegeName"]);
                        objUserMaster.Field = Convert.ToString(dr["Field"]);
                        objUserMaster.Degree = Convert.ToString(dr["Degree"]);
                        objUserMaster.CompanyName = Convert.ToString(dr["CompanyName"]);
                        objUserMaster.Designation = Convert.ToString(dr["Designation"]);
                        objUserMaster.Income = Convert.ToString(dr["Income"]);
                        objUserMaster.IsShortlisted = "S";

                        lstUserMaster.Add(objUserMaster);
                    }
                }
                return View("ShortlistedProfiles", lstUserMaster);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        #endregion

        public ActionResult ManageProfileVisitors(int VisitedUserId, string ActionType)
        {
            List<UserMaster> lstUserMaster = new List<UserMaster>();
            DataSet dsResponse = new DataSet();
            try
            {
                Models.User objUser = (Models.User)(Session["USER"]);
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@VisitedUserId", VisitedUserId);
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                sqlCmd.Parameters.AddWithValue("@Action", ActionType);

                sqlCmd.CommandText = "ManageProfileVisitors";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                if (ActionType == "S")
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        UserMaster objUserMaster = new UserMaster();

                        objUserMaster.UserId = Convert.ToInt32(dr["UserId"]);
                        objUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                        objUserMaster.LastName = Convert.ToString(dr["LastName"]);
                        objUserMaster.Gender = Convert.ToString(dr["Gender"]) == "M" ? "Male" : "Female";
                        objUserMaster.Age = Convert.ToString(dr["Age"]);
                        objUserMaster.DOB = Convert.ToString(dr["DOB"]);
                        objUserMaster.EmailId = Convert.ToString(dr["EmailId"]);
                        objUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);
                        objUserMaster.ProfileId = Convert.ToString(dr["ProfileId"]);

                        objUserMaster.Address1 = Convert.ToString(dr["Address1"]);
                        objUserMaster.Address2 = Convert.ToString(dr["Address2"]);
                        objUserMaster.CityName = Convert.ToString(dr["CityName"]);
                        objUserMaster.StateName = Convert.ToString(dr["StateName"]);
                        objUserMaster.CountryName = Convert.ToString(dr["CountryName"]);
                        objUserMaster.Pincode = Convert.ToString(dr["Pincode"]);
                        objUserMaster.AlternateAddress1 = Convert.ToString(dr["AlternateAddress1"]);
                        objUserMaster.AlternateAddress2 = Convert.ToString(dr["AlternateAddress2"]);
                        objUserMaster.AlternateCityName = "";//Convert.ToString(dr[""]);
                        objUserMaster.AlternateStateName = "";// Convert.ToString(dr[""]);
                        objUserMaster.AlternateCountryName = "";// Convert.ToString(dr[""]);
                        objUserMaster.AlternatePincode = Convert.ToString(dr["AlternatePincode"]);

                        objUserMaster.FatherName = Convert.ToString(dr["FatherName"]);
                        objUserMaster.FatherProfession = Convert.ToString(dr["FatherProfession"]);
                        objUserMaster.MotherName = Convert.ToString(dr["MotherName"]);
                        objUserMaster.MotherProfession = Convert.ToString(dr["MotherProfession"]);

                        objUserMaster.MaritialStatus = Convert.ToString(dr["MaritialStatus"]);
                        objUserMaster.MotherTounge = Convert.ToString(dr["MotherTounge"]);
                        objUserMaster.BirthCountry = Convert.ToString(dr["BirthCountryName"]);
                        objUserMaster.BirthPlace = Convert.ToString(dr["BirthPlace"]);
                        objUserMaster.BirthTime = Convert.ToString(dr["BirthTime"]);
                        objUserMaster.Height = Convert.ToString(dr["Height"]);
                        objUserMaster.BodyType = Convert.ToString(dr["BodyType"]);
                        objUserMaster.SkinTone = Convert.ToString(dr["SkinTone"]);
                        objUserMaster.BloodGroup = Convert.ToString(dr["BloodGroup"]);
                        objUserMaster.IsSmoke = Convert.ToString(dr["IsSmoke"]);
                        objUserMaster.IsDrink = Convert.ToString(dr["IsDrink"]);
                        objUserMaster.IsPhysicalDisable = Convert.ToString(dr["IsPhysicalDisable"]);
                        objUserMaster.CallTime = Convert.ToString(dr["CallTime"]);
                        objUserMaster.ProfileCreatedBy = Convert.ToString(dr["ProfileCreatedBy"]);
                        objUserMaster.ProfilePicPath = "/Uploads/" + objUserMaster.UserId + "/" + Convert.ToString(dr["ProfilePicPath"]);

                        objUserMaster.ReligionName = Convert.ToString(dr["ReligionName"]);
                        objUserMaster.CastName = Convert.ToString(dr["CastName"]);
                        //objUserMaster.SubCastName = "";//Convert.ToString(dr[""]);
                        objUserMaster.MoonSign = Convert.ToString(dr["MoonSign"]);
                        objUserMaster.Star = Convert.ToString(dr["Star"]);
                        objUserMaster.Gotra = Convert.ToString(dr["Gotra"]);

                        objUserMaster.CollegeName = Convert.ToString(dr["CollegeName"]);
                        objUserMaster.Field = Convert.ToString(dr["Field"]);
                        objUserMaster.Degree = Convert.ToString(dr["Degree"]);
                        objUserMaster.CompanyName = Convert.ToString(dr["CompanyName"]);
                        objUserMaster.Designation = Convert.ToString(dr["Designation"]);
                        objUserMaster.Income = Convert.ToString(dr["Income"]);

                        foreach (DataRow dr1 in dsResponse.Tables[1].Rows)
                        {
                            int UserId = Convert.ToInt32(dr1["ShortlistedUserId"]);
                            string shortlistedStatus = Convert.ToString(dr1["Status"]);
                            if (objUserMaster.UserId == UserId && shortlistedStatus == "S")
                                objUserMaster.IsShortlisted = "S";
                            else if (objUserMaster.UserId == UserId && shortlistedStatus == "NS")
                                objUserMaster.IsShortlisted = "NS";
                        }
                        lstUserMaster.Add(objUserMaster);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            if (ActionType == "I")
                return null;
            else
                return View("ProfileVisitors", lstUserMaster);
        }

        public ActionResult SearchProfileById()
        {
            return View();
        }
        public ActionResult SearchProfile()
        {
            Models.User objUser = (Models.User)Session["USER"];
            SearchUserProfile objSearchUserProfile = new SearchUserProfile();
            objSearchUserProfile.ReligionId = objUser.ReligionId;
            objSearchUserProfile.LanguageDetails = new SelectList(LanguageDetails(), "Value", "Text");
            objSearchUserProfile.ReligionDetails = new SelectList(ReligionDetails(), "Value", "Text");
            return View(objSearchUserProfile);
        }
        [HttpPost]
        public ActionResult SearchProfile(SearchUserProfile objSearchUserProfile)
        {
            List<UserMaster> lstUserMaster = new List<UserMaster>();
            DataSet dsResponse = new DataSet();
            try
            {
                Models.User objUser = (Models.User)(Session["USER"]);
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;

                objSearchUserProfile.ReligionId = objSearchUserProfile.ReligionId == "0" ? null : objSearchUserProfile.ReligionId;
                objSearchUserProfile.MotherToungeId = objSearchUserProfile.MotherToungeId == "0" ? null : objSearchUserProfile.MotherToungeId;
                objSearchUserProfile.Income = objSearchUserProfile.Income == "0" ? null : objSearchUserProfile.Income;

                sqlCmd.Parameters.AddWithValue("@FromAge", objSearchUserProfile.FromAge);
                sqlCmd.Parameters.AddWithValue("@ToAge", objSearchUserProfile.ToAge);
                sqlCmd.Parameters.AddWithValue("@ReligionId", objSearchUserProfile.ReligionId);
                sqlCmd.Parameters.AddWithValue("@MotherToungeId", objSearchUserProfile.MotherToungeId);
                sqlCmd.Parameters.AddWithValue("@Income", objSearchUserProfile.Income);
                sqlCmd.Parameters.AddWithValue("@MaritialStatus", objSearchUserProfile.MaritialStatus);
                sqlCmd.Parameters.AddWithValue("@ProfileId", objSearchUserProfile.ProfileId);
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);

                sqlCmd.CommandText = "SearchProfile";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                foreach (DataRow dr in dsResponse.Tables[0].Rows)
                {
                    UserMaster objUserMaster = new UserMaster();

                    objUserMaster.RequestSource = MethodInfo.GetCurrentMethod().Name;
                    objUserMaster.UserId = Convert.ToInt32(dr["UserId"]);
                    objUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                    objUserMaster.LastName = Convert.ToString(dr["LastName"]);
                    objUserMaster.Gender = Convert.ToString(dr["Gender"]) == "M" ? "Male" : "Female";
                    objUserMaster.Age = Convert.ToString(dr["Age"]);
                    objUserMaster.DOB = Convert.ToString(dr["DOB"]);
                    objUserMaster.EmailId = Convert.ToString(dr["EmailId"]);
                    objUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);
                    objUserMaster.ProfileId = Convert.ToString(dr["ProfileId"]);

                    objUserMaster.Address1 = Convert.ToString(dr["Address1"]);
                    objUserMaster.Address2 = Convert.ToString(dr["Address2"]);
                    objUserMaster.CityName = Convert.ToString(dr["CityName"]);
                    objUserMaster.StateName = Convert.ToString(dr["StateName"]);
                    objUserMaster.CountryName = Convert.ToString(dr["CountryName"]);
                    objUserMaster.Pincode = Convert.ToString(dr["Pincode"]);
                    objUserMaster.AlternateAddress1 = Convert.ToString(dr["AlternateAddress1"]);
                    objUserMaster.AlternateAddress2 = Convert.ToString(dr["AlternateAddress2"]);
                    objUserMaster.AlternateCityName = "";//Convert.ToString(dr[""]);
                    objUserMaster.AlternateStateName = "";// Convert.ToString(dr[""]);
                    objUserMaster.AlternateCountryName = "";// Convert.ToString(dr[""]);
                    objUserMaster.AlternatePincode = Convert.ToString(dr["AlternatePincode"]);

                    objUserMaster.FatherName = Convert.ToString(dr["FatherName"]);
                    objUserMaster.FatherProfession = Convert.ToString(dr["FatherProfession"]);
                    objUserMaster.MotherName = Convert.ToString(dr["MotherName"]);
                    objUserMaster.MotherProfession = Convert.ToString(dr["MotherProfession"]);

                    objUserMaster.MaritialStatus = Convert.ToString(dr["MaritialStatus"]);
                    objUserMaster.MotherTounge = Convert.ToString(dr["MotherTounge"]);
                    objUserMaster.BirthCountry = Convert.ToString(dr["BirthCountryName"]);
                    objUserMaster.BirthPlace = Convert.ToString(dr["BirthPlace"]);
                    objUserMaster.BirthTime = Convert.ToString(dr["BirthTime"]);
                    objUserMaster.Height = Convert.ToString(dr["Height"]);
                    objUserMaster.BodyType = Convert.ToString(dr["BodyType"]);
                    objUserMaster.SkinTone = Convert.ToString(dr["SkinTone"]);
                    objUserMaster.BloodGroup = Convert.ToString(dr["BloodGroup"]);
                    objUserMaster.IsSmoke = Convert.ToString(dr["IsSmoke"]);
                    objUserMaster.IsDrink = Convert.ToString(dr["IsDrink"]);
                    objUserMaster.IsPhysicalDisable = Convert.ToString(dr["IsPhysicalDisable"]);
                    objUserMaster.CallTime = Convert.ToString(dr["CallTime"]);
                    objUserMaster.ProfileCreatedBy = Convert.ToString(dr["ProfileCreatedBy"]);
                    objUserMaster.ProfilePicPath = "/Uploads/" + objUserMaster.UserId + "/" + Convert.ToString(dr["ProfilePicPath"]);

                    objUserMaster.ReligionName = Convert.ToString(dr["ReligionName"]);
                    objUserMaster.CastName = Convert.ToString(dr["CastName"]);
                    //objUserMaster.SubCastName = "";//Convert.ToString(dr[""]);
                    objUserMaster.MoonSign = Convert.ToString(dr["MoonSign"]);
                    objUserMaster.Star = Convert.ToString(dr["Star"]);
                    objUserMaster.Gotra = Convert.ToString(dr["Gotra"]);

                    objUserMaster.CollegeName = Convert.ToString(dr["CollegeName"]);
                    objUserMaster.Field = Convert.ToString(dr["Field"]);
                    objUserMaster.Degree = Convert.ToString(dr["Degree"]);
                    objUserMaster.CompanyName = Convert.ToString(dr["CompanyName"]);
                    objUserMaster.Designation = Convert.ToString(dr["Designation"]);
                    objUserMaster.Income = Convert.ToString(dr["Income"]);

                    //foreach (DataRow dr1 in dsResponse.Tables[1].Rows)
                    //{
                    //    int UserId = Convert.ToInt32(dr1["ShortlistedUserId"]);
                    //    string shortlistedStatus = Convert.ToString(dr1["Status"]);
                    //    if (objUserMaster.UserId == UserId && shortlistedStatus == "S")
                    //        objUserMaster.IsShortlisted = "S";
                    //    else if (objUserMaster.UserId == UserId && shortlistedStatus == "NS")
                    //        objUserMaster.IsShortlisted = "NS";
                    //}
                    lstUserMaster.Add(objUserMaster);
                }
            }
            catch (Exception ex)
            { }
            return View("RequestInfo", lstUserMaster);
        }

        public ActionResult UploadPhotos()
        {
            List<ImageUpload> lstImage = new List<ImageUpload>();
            try
            {
                Models.User objUser = (Models.User)Session["USER"];
                DataSet dsResponse = new DataSet();
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                sqlCmd.Parameters.AddWithValue("@Action", "S");
                sqlCmd.CommandText = "UploadImage";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                if (dsResponse != null && dsResponse.Tables.Count > 0 && dsResponse.Tables[0] != null)
                {
                    foreach (DataRow dr in dsResponse.Tables[0].Rows)
                    {
                        ImageUpload objImage = new ImageUpload();
                        objImage.UserId = Convert.ToString(dr["UserId"]);
                        objImage.ImagePath = "/Uploads/" + objImage.UserId + "/" + Convert.ToString(dr["ImagePath"]);
                        objImage.ImageId = Convert.ToString(dr["ImageId"]);

                        lstImage.Add(objImage);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(lstImage);
        }
        [HttpPost]
        public ActionResult UploadPhotos(HttpPostedFileBase imageFile)
        {
            DataSet dsResponse = new DataSet();
            string filename = string.Empty;
            List<ImageUpload> lstImage = new List<ImageUpload>();
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                try
                {
                    Models.User objUser = (Models.User)Session["USER"];
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            filename = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            filename = file.FileName;
                        }
                        string updatedPath = System.IO.Path.Combine(Server.MapPath("~/Uploads/"), objUser.UserId + "\\");
                        if (!System.IO.Directory.Exists(updatedPath))
                        {
                            System.IO.Directory.CreateDirectory(updatedPath);
                        }
                        file.SaveAs(updatedPath + filename);

                        //Save Path to Database

                        string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                        SqlConnection connString = new SqlConnection(conStr);
                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@ImagePath", filename);
                        sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
                        sqlCmd.Parameters.AddWithValue("@Action", "I");
                        sqlCmd.CommandText = "UploadImage";
                        sqlCmd.Connection = connString;
                        SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                        sda.Fill(dsResponse);
                    }

                    if (dsResponse != null && dsResponse.Tables.Count > 0 && dsResponse.Tables[0] != null)
                    {
                        System.Collections.ArrayList arrlst = new System.Collections.ArrayList();
                        foreach (DataRow dr in dsResponse.Tables[0].Rows)
                        {
                            if (!arrlst.Contains(Convert.ToString(dr["ImageId"])))
                            {
                                arrlst.Add(Convert.ToString(dr["ImageId"]));
                                ImageUpload objImage = new ImageUpload();
                                objImage.UserId = Convert.ToString(dr["UserId"]);
                                objImage.ImagePath = "/Uploads/" + objImage.UserId + "/" + Convert.ToString(dr["ImagePath"]);
                                objImage.ImageId = Convert.ToString(dr["ImageId"]);

                                lstImage.Add(objImage);
                            }
                        }
                    }
                    //return Json("Photo Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    //return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                //return Json("No files selected.");
            }
            return View(lstImage);
        }
    }
}