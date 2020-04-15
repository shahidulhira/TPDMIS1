using System.Web.Mvc;

namespace BDCO.Web.Controllers
{
    public class NavbarController : Controller
    {
        //private readonly IQueryDispatcher _query;
        //private readonly ICommandDispatcher _command;

        //public NavbarController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        //{
        //    _query = queryDispatcher;
        //    _command = commandDispatcher;
        //}
        //public ActionResult Index()
        //{           
        //    return PartialView("_Navbar");
        //}

        public ActionResult GetMenu()
        {
            return PartialView("_Navbar");
        }

    }
}