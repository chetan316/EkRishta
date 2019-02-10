using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Models.Custom;

namespace EkRishta.Areas.MobileApp.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult MobileIndex()
        {
            if (Request.Cookies["UserId"] != null && Request.Cookies["UserId"].Value != "")
                return RedirectToAction("MyProfile", "User", new { Area = "MobileApp" });
            else
                return View("~/Areas/MobileApp/Views/Login/MobileIndex.cshtml");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult MobileLogin(Login objLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DataSet dsResult = new DataSet();
                    string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                    SqlConnection connString = new SqlConnection(conStr);
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@MobileNo", objLogin.MobileNo);
                    sqlCmd.Parameters.AddWithValue("@Password", objLogin.LoginPassword);
                    sqlCmd.CommandText = "UserLogin";
                    sqlCmd.Connection = connString;
                    SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                    sda.Fill(dsResult);

                    if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                    {
                        User objUser = new User();
                        objUser.UserId = Convert.ToInt32(dsResult.Tables[0].Rows[0]["UserId"]);
                        objUser.UserName = Convert.ToString(dsResult.Tables[0].Rows[0]["FirstName"]) + " " + Convert.ToString(dsResult.Tables[0].Rows[0]["LastName"]);
                        objUser.ProfileId = Convert.ToString(dsResult.Tables[0].Rows[0]["ProfileId"]);
                        objUser.MobileNo = Convert.ToString(dsResult.Tables[0].Rows[0]["MobileNo"]);
                        objUser.EmailId = Convert.ToString(dsResult.Tables[0].Rows[0]["EmailId"]);
                        objUser.ReligionId = Convert.ToString(dsResult.Tables[0].Rows[0]["ReligionId"]);
                        objUser.ProfilePicPath = "/Uploads/" + objUser.UserId + "/" + Convert.ToString(dsResult.Tables[0].Rows[0]["ProfilePicPath"]);

                        //Session["USER"] = objUser;
                        //HttpCookie SessionOut = new HttpCookie("UserId");
                        //SessionOut.Expires = DateTime.Now.AddYears(-365);
                        //Response.Cookies.Add(SessionOut);

                        HttpCookie UserId = new HttpCookie("UserId");
                        UserId.Value = Convert.ToString(objUser.UserId);
                        UserId.Expires = DateTime.Now.AddYears(365);
                        Response.Cookies.Add(UserId);

                        HttpCookie UserName = new HttpCookie("UserName");
                        UserName.Value = Convert.ToString(objUser.UserName);
                        UserName.Expires = DateTime.Now.AddYears(365);
                        Response.Cookies.Add(UserName);

                        HttpCookie ProfileId = new HttpCookie("ProfileId");
                        ProfileId.Value = Convert.ToString(objUser.ProfileId);
                        ProfileId.Expires = DateTime.Now.AddYears(365);
                        Response.Cookies.Add(ProfileId);

                        HttpCookie ProfilePicPath = new HttpCookie("ProfilePicPath");
                        ProfilePicPath.Value = Convert.ToString(objUser.ProfilePicPath);
                        ProfilePicPath.Expires = DateTime.Now.AddYears(365);
                        Response.Cookies.Add(ProfilePicPath);

                        HttpCookie MobileNo = new HttpCookie("MobileNo");
                        MobileNo.Value = Convert.ToString(objUser.MobileNo);
                        MobileNo.Expires = DateTime.Now.AddYears(365);
                        Response.Cookies.Add(MobileNo);

                        HttpCookie EmailId = new HttpCookie("EmailId");
                        EmailId.Value = Convert.ToString(objUser.EmailId);
                        EmailId.Expires = DateTime.Now.AddYears(365);
                        Response.Cookies.Add(EmailId);

                        HttpCookie ReligionId = new HttpCookie("ReligionId");
                        ReligionId.Value = Convert.ToString(objUser.ReligionId);
                        ReligionId.Expires = DateTime.Now.AddYears(365);
                        Response.Cookies.Add(ReligionId);
                      
                        return RedirectToAction("MyProfile", "User", new { Area = "MobileApp" });
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Please Enter Valid UserId and Password.";
                        return View("MobileIndex");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Please Enter Valid UserId and Password.";
                    return View("MobileIndex");
                }
            }
            catch (Exception ex)
            {
                WriteLog("Message : " + ex.Message + "\n" + "StackTrace : " + ex.StackTrace);
                ViewBag.ErrorMessage = "Error Occurred, Please Try Again.";
                return View("MobileIndex");
            }
        }

        [HttpGet]
        public ActionResult MobileLogOut()
        {
            HttpCookie SessionOut = new HttpCookie("UserId");
            SessionOut.Expires = DateTime.Now.AddYears(-365);
            Response.Cookies.Add(SessionOut);
            return View("MobileIndex");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult SendForgotPasswordOTP(string MobileNo)
        {
            DataSet dsResponse = new DataSet();
            string APIResponse = string.Empty, response = string.Empty;
            try
            {
                var result = ValidateMobileNo(MobileNo);

                string strResponse = JsonConvert.SerializeObject(result);
                var strData = JObject.Parse(strResponse);
                if (Convert.ToString(strData["Data"]) != string.Empty)
                {
                    string OTP = string.Empty;
                    string ForgotPwdSMSText = ConfigurationManager.AppSettings["ForgotPwdSMSText"];
                    APIResponse = SendOTPAPI(MobileNo, ForgotPwdSMSText, out OTP);

                    if (!APIResponse.ToLower().Contains("error"))
                    {
                        response = SaveForgotPwdOTPDetails(MobileNo, OTP);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangePassword(ForgotPassword objForgotPassword)
        {
            string result = string.Empty;
            try
            {

                DataSet dsResult = new DataSet();
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@MobileNo", objForgotPassword.MobileNo);
                sqlCmd.Parameters.AddWithValue("@Password", objForgotPassword.Password);
                sqlCmd.Parameters.AddWithValue("@OTP", objForgotPassword.OTP);
                sqlCmd.CommandText = "ChangePassword";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResult);

                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    result = Convert.ToString(dsResult.Tables[0].Rows[0]["Result"]);
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}