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

                        objUserMaster.CollegeName = Convert.ToString(dr["CollegeName"]);
                        objUserMaster.Field = Convert.ToString(dr["Field"]);
                        objUserMaster.Degree = Convert.ToString(dr["Degree"]);
                        objUserMaster.CompanyName = Convert.ToString(dr["CompanyName"]);
                        objUserMaster.Designation = Convert.ToString(dr["Designation"]);
                        objUserMaster.Income = Convert.ToString(dr["Income"]);
                    }
                }

                if (UserId == null)
                    return View("~/Areas/MobileApp/Views/User/MyProfile.cshtml", objUserMaster);
                else
                    return RedirectToAction("ViewProfile", objUserMaster);//"~/Areas/MobileApp/Views/User/ViewProfile.cshtml", objUserMaster);

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
            return View("~/Areas/MobileApp/Views/User/RequestInfo.cshtml", lstUserMaster);
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
                sqlCmd.Parameters.AddWithValue("@ReligionId", objUser.ReligionId);
                sqlCmd.CommandText = "GetSentRequest";
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
            return View("~/Areas/MobileApp/Views/User/RequestInfo.cshtml", lstUserMaster);
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
            return View("~/Areas/MobileApp/Views/User/ProfileRequest.cshtml", lstUserMaster);
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
                return View("~/Areas/MobileApp/Views/User/ViewProfile.cshtml", objUserMaster);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}