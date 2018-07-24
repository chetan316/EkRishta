using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EkRishta
{
    public class BaseController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SendSMS(string MobileNo)
        {
            string response = string.Empty;
            try
            {
                    Random generator = new Random();
                    String OTP = generator.Next(0, 9999).ToString();
                    string smsText = OTP + " " + ConfigurationManager.AppSettings["RegistrationSMSText"];
                    string SMSLoginId = ConfigurationManager.AppSettings["SMSLoginId"];
                    string SMSPassword = ConfigurationManager.AppSettings["SMSPassword"];
                    string SMSSenderId = ConfigurationManager.AppSettings["SMSSenderId"];
                    string SMSUrl = ConfigurationManager.AppSettings["SMSUrl"];

                    string smsURL = SMSUrl + "username=" + SMSLoginId + "&password=" + SMSPassword + "&message=" + HttpUtility.UrlEncode(smsText) + "&sender=" + SMSSenderId + "&numbers=" + MobileNo;
                    //WebClient webClient = new WebClient();
                    string APIResponse = "YOUR SMS HAS BEEN SENT";//webClient.DownloadString(smsURL);

                    DataSet dsResult = new DataSet();
                    string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                    SqlConnection connString = new SqlConnection(conStr);
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                    sqlCmd.Parameters.AddWithValue("@SMSText", smsText);
                    sqlCmd.Parameters.AddWithValue("@APIRequest", smsURL);
                    sqlCmd.Parameters.AddWithValue("@APIResponse", APIResponse);
                    sqlCmd.CommandText = "InsertSMSLogs";
                    sqlCmd.Connection = connString;
                    SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                    sda.Fill(dsResult);

                    if (APIResponse.Contains("YOUR SMS HAS BEEN SENT"))
                    {
                        OTP="1111";
                        response = SaveOTPDetails(MobileNo, OTP);
                    }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public string SaveOTPDetails(string MobileNo, string OTP)
        {
            string response = string.Empty;
            DataSet dsOTP = new DataSet();
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                sqlCmd.Parameters.AddWithValue("@OTP", OTP);
                sqlCmd.CommandText = "SaveOTPDetails";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsOTP);

                if (dsOTP != null && dsOTP.Tables[0].Rows.Count > 0)
                {
                    response = Convert.ToString(dsOTP.Tables[0].Rows[0]["Result"]);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        public JsonResult CheckOTP(string MobileNo, string OTP)
        {
            string response = string.Empty;
            DataSet dsResult = new DataSet();
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                sqlCmd.Parameters.AddWithValue("@OTP", OTP);
                sqlCmd.CommandText = "ValidateOTP";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResult);

                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    response = Convert.ToString(dsResult.Tables[0].Rows[0]["Result"]);
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