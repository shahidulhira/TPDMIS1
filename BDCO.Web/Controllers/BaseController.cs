using BDCO.Web.Helpers;
using BDCO.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BDCO.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
            {
               // cultureName = cultureCookie.Value != "" ? cultureCookie.Value : User.Language();
            }

            else
            {
              //  cultureName = User.Language(); // obtain it from HTTP header AcceptLanguages
            }

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe


            // Modify current thread's cultures 
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureName);


            return base.BeginExecuteCore(callback, state);
        }
    }
}