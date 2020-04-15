using BDCO.Core.Command;
using BDCO.Domain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BDCO.Web.Controllers
{
    public class HomeController : BaseController
    {
        //private readonly IQueryDispatcher _query;
        //private readonly ICommandDispatcher _command;
        //DatabaseContext context = new DatabaseContext();
        //public HomeController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        //{
        //    _query = queryDispatcher;
        //    _command = commandDispatcher;
        //}
        [Authorize]
        public ActionResult ExportCSV()
        {           
            return View();
        }
      

        public ActionResult TestApp()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        

        [AllowAnonymous]
        public ActionResult GetPdf()
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();

            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }

            return View("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}