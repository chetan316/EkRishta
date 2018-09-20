using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Areas.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EkRishta.Areas.MobileApp.Controllers
{
    public class UserPreferencesController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetPreference()
        {
            UserPreference objUserPreference = new UserPreference();
            try
            {
                objUserPreference.ReligionDetails = new SelectList(ReligionDetails(), "Value", "Text");
                objUserPreference.LanguageDetails = new SelectList(LanguageDetails(), "Value", "Text");
                objUserPreference.StateDetails = new SelectList(StateDetails(), "Value", "Text");
                objUserPreference.CityDetails = new SelectList(CityDetails(), "Value", "Text");
                objUserPreference.CasteDetails = new SelectList(CastDetails(), "Value", "Text");
            }
            catch (Exception ex)
            {
                
            }
            return View(objUserPreference);
        }

        [HttpPost]
        public ActionResult SetPreference(UserPreference objUserPreference)
        {
            DataSet dsResponse = new DataSet();
            try
            {
                Models.User objUser = (Models.User)(Session["USER"]);
                objUserPreference.UserId = objUser.UserId;
                objUserPreference.Action = "I";

                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@FromAge", objUserPreference.FromAge);
                sqlCmd.Parameters.AddWithValue("@ToAge", objUserPreference.ToAge);
                sqlCmd.Parameters.AddWithValue("@FromHeight", objUserPreference.FromHeight);
                sqlCmd.Parameters.AddWithValue("@ToHeight", objUserPreference.ToHeight);
                sqlCmd.Parameters.AddWithValue("@MaritialStatus", objUserPreference.MaritialStatus);
                sqlCmd.Parameters.AddWithValue("@CityId", objUserPreference.CityId);
                //sqlCmd.Parameters.AddWithValue("@CountryId", objUserPreference.CountryId);
                sqlCmd.Parameters.AddWithValue("@ReligionId", objUserPreference.ReligionId);
                sqlCmd.Parameters.AddWithValue("@CasteId", objUserPreference.CasteId);
                sqlCmd.Parameters.AddWithValue("@MotherToungeId", objUserPreference.MotherToungeId);
                sqlCmd.Parameters.AddWithValue("@Income", objUserPreference.Income);
                //sqlCmd.Parameters.AddWithValue("@Diet", objUserPreference.Diet);
                sqlCmd.Parameters.AddWithValue("@IsDrink", objUserPreference.IsDrink);
                sqlCmd.Parameters.AddWithValue("@IsSmoke", objUserPreference.IsSmoke);
                sqlCmd.Parameters.AddWithValue("@IsPhysicalDisable", objUserPreference.IsPhysicalDisable);
                sqlCmd.Parameters.AddWithValue("@SkinTone", objUserPreference.SkinTone);
                sqlCmd.Parameters.AddWithValue("@BodyType", objUserPreference.BodyType);
                sqlCmd.Parameters.AddWithValue("@Action", objUserPreference.Action);
                sqlCmd.Parameters.AddWithValue("@UserId", objUserPreference.UserId);

                sqlCmd.CommandText = "ManageUserPreferences";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                //if(dsResponse)
                return RedirectToAction("ManageUserPreferences");
            }
            catch (Exception ex)
            {}
            return View();
        }

        public ActionResult ManageUserPreferences(UserPreference objUserPreference)
        {
            DataSet dsResponse = new DataSet();
            UserPreference objPreference = new UserPreference();

            try
            {
                Models.User objUser = (Models.User)(Session["USER"]);
                objUserPreference.UserId = objUser.UserId;
                objUserPreference.Action = "S";

                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@UserId", objUserPreference.UserId);
                sqlCmd.Parameters.AddWithValue("@Action", objUserPreference.Action);
                sqlCmd.CommandText = "ManageUserPreferences";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);

                if (dsResponse != null && dsResponse.Tables.Count > 0 && dsResponse.Tables[0].Rows.Count > 0)
                {
                    objPreference.ReligionDetails = new SelectList(ReligionDetails(), "Value", "Text");
                    objPreference.LanguageDetails = new SelectList(LanguageDetails(), "Value", "Text");
                    objPreference.StateDetails = new SelectList(StateDetails(), "Value", "Text");
                    objPreference.CityDetails = new SelectList(CityDetails(), "Value", "Text");
                    objPreference.CasteDetails = new SelectList(CastDetails(), "Value", "Text");

                    objPreference.UserPreferenceId = Convert.ToInt32(dsResponse.Tables[0].Rows[0]["UserPreferenceId"]);
                    objPreference.FromAge = Convert.ToString(dsResponse.Tables[0].Rows[0]["FromAge"]);
                    objPreference.ToAge = Convert.ToString(dsResponse.Tables[0].Rows[0]["ToAge"]);
                    objPreference.FromHeight = Convert.ToString(dsResponse.Tables[0].Rows[0]["FromHeight"]);
                    objPreference.ToHeight = Convert.ToString(dsResponse.Tables[0].Rows[0]["ToHeight"]);
                    objPreference.MaritialStatus = Convert.ToString(dsResponse.Tables[0].Rows[0]["MaritialStatus"]);
                    objPreference.CityId = Convert.ToInt32(dsResponse.Tables[0].Rows[0]["CityId"]);
                    objPreference.CityName = Convert.ToString(dsResponse.Tables[0].Rows[0]["CityName"]);
                    objPreference.CountryId = Convert.ToInt32(dsResponse.Tables[0].Rows[0]["CountryId"]);
                    objPreference.CountryName = Convert.ToString(dsResponse.Tables[0].Rows[0]["CountryName"]);
                    objPreference.ReligionId = Convert.ToString(dsResponse.Tables[0].Rows[0]["ReligionId"]);
                    objPreference.ReligionName = Convert.ToString(dsResponse.Tables[0].Rows[0]["ReligionName"]);
                    objPreference.CasteId = Convert.ToInt32(dsResponse.Tables[0].Rows[0]["CasteId"]);
                    objPreference.CasteName = Convert.ToString(dsResponse.Tables[0].Rows[0]["CasteName"]);
                    objPreference.MotherToungeId = Convert.ToString(dsResponse.Tables[0].Rows[0]["MotherToungeId"]);
                    objPreference.MotherTounge = Convert.ToString(dsResponse.Tables[0].Rows[0]["MotherTounge"]);
                    objPreference.Income = Convert.ToString(dsResponse.Tables[0].Rows[0]["Income"]);
                    objPreference.IsDrink = Convert.ToString(dsResponse.Tables[0].Rows[0]["IsDrink"]);
                    objPreference.IsSmoke = Convert.ToString(dsResponse.Tables[0].Rows[0]["IsSmoke"]);
                    objPreference.IsPhysicalDisable = Convert.ToString(dsResponse.Tables[0].Rows[0]["IsPhysicalDisable"]);
                    objPreference.SkinTone = Convert.ToString(dsResponse.Tables[0].Rows[0]["SkinTone"]);
                    objPreference.BodyType = Convert.ToString(dsResponse.Tables[0].Rows[0]["BodyType"]);
                }
            }
            catch (Exception ex)
            {
            }
            return View(objPreference);
        }
    }
}