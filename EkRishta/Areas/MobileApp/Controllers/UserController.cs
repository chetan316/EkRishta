using Models.Custom;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EkRishta.Areas.MobileApp.Controllers
{
    [SessionAuthorize]
    public class UserController : BaseController
    {
        public ActionResult MyProfile()
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
                Models.User objUser = (Models.User)(Session["USER"]);
                sqlCmd.Parameters.AddWithValue("@UserId", objUser.UserId);
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
                        objUserMaster.Gender = Convert.ToString(dr["Gender"])=="M"?"Male":"Female";
                        objUserMaster.Age = Convert.ToString(dr["Age"]);
                        objUserMaster.DOB = Convert.ToString(dr["DOB"]);
                        objUserMaster.EmailId = Convert.ToString(dr["EmailId"]);
                        objUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);

                        objUserMaster.Address1 = Convert.ToString(dr["Address1"]);
                        objUserMaster.Address2 = Convert.ToString(dr["Address2"]);
                        objUserMaster.CityName = "";// Convert.ToString(dr[""]);
                        objUserMaster.StateName = "";//Convert.ToString(dr[""]);
                        objUserMaster.CountryName = "India";// Convert.ToString(dr[""]);
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

                        objUserMaster.MaritialStatus = Convert.ToString(dr["MaritialStatus"])=="1"?"Unmarried":"Married";
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
                        objUserMaster.ProfilePicPath = Server.MapPath("~/Uploads/" + objUser.UserId + "/") + Convert.ToString(dr["ProfilePicPath"]);

                        objUserMaster.ReligionName = "Hindu";// Convert.ToString(dr[""]);
                        objUserMaster.CastName = "Maratha";//"Convert.ToString(dr[""]);
                        objUserMaster.SubCastName = "";//Convert.ToString(dr[""]);
                        objUserMaster.MoonSign = Convert.ToString(dr["MoonSign"]);
                        objUserMaster.Star = Convert.ToString(dr["Star"]);
                        objUserMaster.Gotra = Convert.ToString(dr["Gotra"]);

                        objUserMaster.CollegeName = Convert.ToString(dr["CollegeName"]);
                        objUserMaster.Field = Convert.ToString(dr["Field"]);
                        objUserMaster.Degree = Convert.ToString(dr["Degree"]);
                        objUserMaster.CompanyName = Convert.ToString(dr["CompanyName"]);
                        objUserMaster.Designation = Convert.ToString(dr["Designation"]);
                        objUserMaster.Income = Convert.ToString(dr["Income"]) == "1" ? "INR Upto 1 Lakh" : "INR 2-4 Lakh";
                    }
                }
                return View("MyProfile", objUserMaster);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}