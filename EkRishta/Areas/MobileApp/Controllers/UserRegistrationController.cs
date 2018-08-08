using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace EkRishta.Areas.MobileApp.Controllers
{
    public class UserRegistrationController : BaseController
    {
        public ActionResult RegisterUser()
        {
            UserRegistration objUserRegistration = new UserRegistration();
            objUserRegistration.ReligionDetails = new SelectList(ReligionDetails(), "Value", "Text");
            objUserRegistration.LanguageDetails = new SelectList(LanguageDetails(), "Value", "Text");
            objUserRegistration.StateDetails = new SelectList(StateDetails(), "Value", "Text");
            objUserRegistration.DOBDayDetails = new SelectList(DOBDayDetails(), "Value", "Text");
            return View(objUserRegistration);
        }

        [HttpPost]
        public ActionResult RegisterUser(UserRegistration objUserRegistration)
        {
            string response = string.Empty;
            string userId = string.Empty;
            DataSet dsResponse = new DataSet();
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@FirstName", objUserRegistration.FirstName);
                sqlCmd.Parameters.AddWithValue("@LastName", objUserRegistration.LastName);
                sqlCmd.Parameters.AddWithValue("@Gender", objUserRegistration.Gender);
                sqlCmd.Parameters.AddWithValue("@MobileNo", objUserRegistration.MobileNo);
                sqlCmd.Parameters.AddWithValue("@EmailId", objUserRegistration.EmailId);
                sqlCmd.Parameters.AddWithValue("@Password", objUserRegistration.Password);
                sqlCmd.Parameters.AddWithValue("@MaritialStatus", objUserRegistration.MaritialStatus);
                sqlCmd.Parameters.AddWithValue("@MotherTounge", objUserRegistration.MotherTounge);
                sqlCmd.Parameters.AddWithValue("@CallTime", objUserRegistration.CallTime);
                sqlCmd.Parameters.AddWithValue("@ProfileCreatedBy", objUserRegistration.ProfileCreatedBy);
                sqlCmd.Parameters.AddWithValue("@Income", objUserRegistration.Income);
                sqlCmd.Parameters.AddWithValue("@ReligionId", objUserRegistration.ReligionId);
                sqlCmd.Parameters.AddWithValue("@StateId", objUserRegistration.StateId);
                sqlCmd.Parameters.AddWithValue("@CityId", objUserRegistration.CityId);
                sqlCmd.Parameters.AddWithValue("@DOB", objUserRegistration.DOBDay);
                sqlCmd.CommandText = "RegisterUser";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                if (dsResponse != null && dsResponse.Tables[0] != null && dsResponse.Tables[0].Rows.Count > 0)
                {
                    response = Convert.ToString(dsResponse.Tables[0].Rows[0]["Result"]);
                    userId = Convert.ToString(dsResponse.Tables[0].Rows[0]["UserId"]);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //return Json(response,JsonRequestBehavior.AllowGet);
            return Json(new
            {
                Status = response,
                UserId = userId
            });
        }

        [HttpPost]
        public ActionResult UploadFiles(UserRegistration objUserRegistration)
        {
            string fname = string.Empty;

            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        // Get the complete folder path and store the file inside it.
                        string updatedPath = System.IO.Path.Combine(Server.MapPath("~/Uploads/"), objUserRegistration.UserId + "\\");
                        //fname = updatedPath+ fname;
                        if (!System.IO.Directory.Exists(updatedPath))
                        {
                            System.IO.Directory.CreateDirectory(updatedPath);
                        }
                        file.SaveAs(updatedPath + fname);
                    }

                    //Save Path to Database
                    DataSet dsResponse = new DataSet();
                    string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                    SqlConnection connString = new SqlConnection(conStr);
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@ProfiePicPath", fname);
                    sqlCmd.Parameters.AddWithValue("@UserId", objUserRegistration.UserId);
                    sqlCmd.CommandText = "UpdateUserDetails";
                    sqlCmd.Connection = connString;
                    SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                    sda.Fill(dsResponse);

                    if (dsResponse != null && dsResponse.Tables[0] != null && dsResponse.Tables[0].Rows.Count > 0)
                    {
                        User objUser = new User();
                        objUser.UserId = objUserRegistration.UserId;
                        objUser.ProfileId = Convert.ToString(dsResponse.Tables[0].Rows[0]["ProfileId"]);
                        objUser.MobileNo = Convert.ToString(dsResponse.Tables[0].Rows[0]["MobileNo"]);
                        objUser.EmailId = Convert.ToString(dsResponse.Tables[0].Rows[0]["EmailId"]);
                        objUser.ReligionId = Convert.ToString(dsResponse.Tables[0].Rows[0]["ReligionId"]);
                        objUser.ShareCount = 0;

                        Session["USER"] = objUser;
                    }

                    // Returns message that successfully uploaded  
                    return Json("Photo Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult ValidateMobileNo(string MobileNo)
        {
            string response = string.Empty;
            DataSet dsResponse = new DataSet();
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                //                sqlCmd.Parameters.AddWithValue("@CityId", objUserRegistration.CityId);
                sqlCmd.CommandText = "ValidateMobileNo";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                if (dsResponse != null && dsResponse.Tables[0].Rows.Count > 0)
                {
                    response = Convert.ToString(dsResponse.Tables[0].Rows[0]["MobileNo"]);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}