using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EkRishta.Areas.MobileApp.Controllers
{
    public class SitePolicyController : Controller
    {
        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult TermsAndConditions()
        {
            return View();
        }

        public ActionResult Disclaimer()
        {
            return View();
        }
    }
}