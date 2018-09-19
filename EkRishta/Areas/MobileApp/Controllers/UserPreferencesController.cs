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
                return View("UserPreferences");
            }
            catch (Exception ex)
            {

            }
            
        }

        public ActionResult UserPreferences()
        {
            DataSet dsResponse = new DataSet();
            UserPreference objUserPreference = new UserPreference();
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
            }
            catch (Exception ex)
            {
            }
            return View(objUserPreference);
        }
    }
}