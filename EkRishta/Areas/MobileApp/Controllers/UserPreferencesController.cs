using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            try
            {

            }
            catch (Exception ex)
            {
                
                throw;
            }
            return View();
        }
    }
}