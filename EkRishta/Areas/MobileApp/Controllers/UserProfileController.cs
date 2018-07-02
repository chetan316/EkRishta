using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EkRishta.Areas.MobileApp.Controllers
{
    [SessionAuthorize]
    public class UserProfileController : BaseController
    {
        public ActionResult MyProfile()
        {
            return View();
        }
    }
}