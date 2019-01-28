using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EkRishta.Areas.WebApp.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult WebIndex()
        {
            //return View("~/Areas/WebApp/Views/Login/WebIndex.cshtml");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WebLogin(Login objLogin)
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

                        Session["USER"] = objUser;
                        return RedirectToAction("MyProfile", "User", new { Area = "MobileApp" });
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Please Enter Valid UserId and Password.";
                        return View("WebIndex");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Please Enter Valid UserId and Password.";
                    return View("WebIndex");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error Occurred, Please Try Again.";
                return View("WebIndex");
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Remove("USER");
            return View("WebIndex");
        }
    }
}