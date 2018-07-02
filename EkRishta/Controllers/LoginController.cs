using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EkRishta.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login objLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DataSet dsResult = new DataSet();
                    string conStr = ConfigurationManager.ConnectionStrings["DBConString"].ConnectionString;
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
                        objUser.ProfileId = Convert.ToString(dsResult.Tables[0].Rows[0]["ProfileId"]);
                        objUser.MobileNo = Convert.ToString(dsResult.Tables[0].Rows[0]["MobileNo"]);
                        objUser.EmailId = Convert.ToString(dsResult.Tables[0].Rows[0]["EmailId"]);

                        Session["USER"] = objUser;
                    }
                    return View("Index");
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                return View("Index");

                throw;
            }
        }
    }
}