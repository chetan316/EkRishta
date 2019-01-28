using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EkRishta
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ViewBag.JSRefNo = ConfigurationManager.AppSettings["JSRefNo"];
        }
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SendSMS(string MobileNo)
        {
            string APIResponse = string.Empty, response=string.Empty;
            try
            {
                Random generator = new Random();
                String OTP = generator.Next(0, 9999).ToString();
                string smsText = ConfigurationManager.AppSettings["RegistrationSMSText"] + OTP;
                string SMSLoginId = ConfigurationManager.AppSettings["SMSLoginId"];
                string SMSPassword = ConfigurationManager.AppSettings["SMSPassword"];
                string SMSSenderId = ConfigurationManager.AppSettings["SMSSenderId"];
                string SMSUrl = ConfigurationManager.AppSettings["SMSUrl"];
                
                string smsURL = SMSUrl+"username=" + SMSLoginId + "&password=" + SMSPassword + "&from=" + SMSSenderId + "&to=" + MobileNo + "&text=" + smsText + "&coding=0";
                WebClient webClient = new WebClient();
                APIResponse = webClient.DownloadString(smsURL);//"YOUR SMS HAS BEEN SENT";
                

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

                if (!APIResponse.ToLower().Contains("error"))
                {
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

        public List<SelectListItem> ReligionDetails()
        {
            DataSet dsReligion = new DataSet();
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "GetReligionMaster";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsReligion);

                if (dsReligion != null && dsReligion.Tables[0].Rows.Count > 0)
                {
                    lstSelectItem.Add(new SelectListItem { Text = "Religion", Value = "0" });
                    foreach (DataRow dr in dsReligion.Tables[0].Rows)
                    {
                        lstSelectItem.Add(new SelectListItem { Text = Convert.ToString(dr["ReligionName"]), Value = Convert.ToString(dr["ReligionId"]) });
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return lstSelectItem;
        }

        public List<SelectListItem> CastDetails()
        {
            DataSet dsCast = new DataSet();
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "GetCastMaster";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsCast);

                if (dsCast != null && dsCast.Tables[0].Rows.Count > 0)
                {
                    lstSelectItem.Add(new SelectListItem { Text = "Select Cast", Value = "0" });
                    foreach (DataRow dr in dsCast.Tables[0].Rows)
                    {
                        lstSelectItem.Add(new SelectListItem { Text = Convert.ToString(dr["CastName"]), Value = Convert.ToString(dr["CastId"]) });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstSelectItem;
        }
        public List<SelectListItem> LanguageDetails()
        {
            DataSet dsLanguage = new DataSet();
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "GetLanguageMaster";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsLanguage);

                if (dsLanguage != null && dsLanguage.Tables[0].Rows.Count > 0)
                {
                    lstSelectItem.Add(new SelectListItem { Text = "Mother Tounge", Value = "0" });
                    foreach (DataRow dr in dsLanguage.Tables[0].Rows)
                    {
                        lstSelectItem.Add(new SelectListItem { Text = Convert.ToString(dr["LanguageName"]), Value = Convert.ToString(dr["LanguageId"]) });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstSelectItem;
        }

        public List<SelectListItem> StateDetails()
        {
            DataSet dsState = new DataSet();
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "GetStateMaster";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsState);

                if (dsState != null && dsState.Tables[0].Rows.Count > 0)
                {
                    lstSelectItem.Add(new SelectListItem { Text = "Select State", Value = "0" });
                    foreach (DataRow dr in dsState.Tables[0].Rows)
                    {
                        lstSelectItem.Add(new SelectListItem { Text = Convert.ToString(dr["StateName"]), Value = Convert.ToString(dr["StateId"]) });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstSelectItem;
        }

        public List<SelectListItem> CityDetails()
        {
            DataSet dsCity = new DataSet();
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "GetCityMaster";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsCity);

                if (dsCity != null && dsCity.Tables[0].Rows.Count > 0)
                {
                    lstSelectItem.Add(new SelectListItem { Text = "Select City", Value = "0" });
                    foreach (DataRow dr in dsCity.Tables[0].Rows)
                    {
                        lstSelectItem.Add(new SelectListItem { Text = Convert.ToString(dr["CityName"]), Value = Convert.ToString(dr["CityId"]) });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstSelectItem;
        }
        public List<SelectListItem> CountryDetails()
        {
            DataSet dsCountry = new DataSet();
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "GetCountryMaster";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsCountry);

                if (dsCountry != null && dsCountry.Tables[0].Rows.Count > 0)
                {
                    lstSelectItem.Add(new SelectListItem { Text = "Select Country", Value = "0" });
                    foreach (DataRow dr in dsCountry.Tables[0].Rows)
                    {
                        lstSelectItem.Add(new SelectListItem { Text = Convert.ToString(dr["CountryName"]), Value = Convert.ToString(dr["CountryId"]) });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstSelectItem;
        }
        public List<SelectListItem> DOBDayDetails()
        {
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            lstSelectItem.Add(new SelectListItem { Text = "Day", Value = "0" });
            for (int i = 1; i <= 31; i++)
            {
                lstSelectItem.Add(new SelectListItem { Text = i.ToString().Length < 2 ? "0" + i.ToString() : i.ToString(), Value = i.ToString().Length < 2 ? "0" + i.ToString() : i.ToString() });
            }
            return lstSelectItem;
        }

        public List<SelectListItem> DOBMonthDetails()
        {
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            lstSelectItem.Add(new SelectListItem { Text = "Month", Value = "0" });
            for (int i = 1; i <= 12; i++)
            {
                lstSelectItem.Add(new SelectListItem { Text = i.ToString().Length < 2 ? "0" + i.ToString() : i.ToString(), Value = i.ToString().Length < 2 ? "0" + i.ToString() : i.ToString() });
            }
            return lstSelectItem;
        }

        public List<SelectListItem> DOBYearDetails()
        {
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            int startYear = DateTime.Now.Year - 47;
            int endYear = DateTime.Now.Year - 18;
            lstSelectItem.Add(new SelectListItem { Text = "Year", Value = "0" });
            for (int i = startYear; i <= endYear; i++)
            {
                lstSelectItem.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            return lstSelectItem;
        }

        public void WriteLog(string strMessage)
        {
            try
            {
                string strLogPath = ConfigurationManager.AppSettings["LogPath"];
                if (!System.IO.Directory.Exists(strLogPath))
                    System.IO.Directory.CreateDirectory(strLogPath);

                string strFileName = "Logs_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".log";

                FileStream fs = new FileStream(strLogPath + "\\" + strFileName,
                                    FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine(strMessage + "\n");
                m_streamWriter.Flush();
                m_streamWriter.Close();
            }
            catch (Exception)
            {
            }
        }
    }


}