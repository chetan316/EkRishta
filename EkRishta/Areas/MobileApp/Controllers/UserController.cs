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
                        objUserMaster.BirthCountry = Convert.ToString(dr["BirthCountry"]);
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

        public ActionResult AcceptedRequest()
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
                sqlCmd.Parameters.AddWithValue("@RequestStatus", "Accepted");
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
                sqlCmd.Parameters.AddWithValue("@RequestStatus", "Blocked");
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

                UserMaster objUserMaster = new UserMaster();

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
                        objUserMaster.BirthCountry = Convert.ToString(dr["BirthCountry"]);
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
                }

                //if (UserId == null)
                //    return View("~/Areas/MobileApp/Views/User/MyProfile.cshtml", objUserMaster);
                //else
                //    return View("~/Areas/MobileApp/Views/User/ViewProfile.cshtml", objUserMaster);
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
                
                return PartialView("_BasicDetails", objBasicDetails);
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

                return PartialView("_ProfessionalDetails", objProfessionalDetails);
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

        #endregion
    }
}