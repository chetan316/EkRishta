using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EkRishta
{
    public class SessionAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return System.Web.HttpContext.Current.Request.Cookies["UserId"] != null && System.Web.HttpContext.Current.Request.Cookies["UserId"].Value != "";
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/MobileApp/Login/MobileIndex");
        }
    }
}