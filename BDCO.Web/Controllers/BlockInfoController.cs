
using BDCO.Domain.Models;
using System.Web.Mvc;

namespace BDCO.Web.Controllers
{
    public class BlockInfoController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveOrUpdate(BlockInfo block)
        {
            return Json(block.Save(block), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Getblock(BlockStoreProcedure block)
        {
            return Json(new BlockInfo().Searchblock(block), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetblockById(int id)
        {
            return Json(new BlockInfo().GetSingle(id), JsonRequestBehavior.AllowGet);
        }
    }
}