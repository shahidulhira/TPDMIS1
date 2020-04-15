using BDCO.Domain;
using BDCO.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDCO.Web.Controllers
{
    public class DocAndRecordChecklistController : Controller
    {
        readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            string sql = $"EXEC [spGetDocAndRecordChecklistResult]";
            var result = _unitOfWork.GenericRepositories<DocAndRecordChecklistResults>().GetRecordSet(sql).ToList();
            return View(result);
        }

        [HttpPost]
        public JsonResult GetDocAndRecordChecklist(int PageSize, int PageNo)
        {   
            try
            {
                DocAndRecordChecklist doc = new DocAndRecordChecklist();
                string sql = $"EXEC spGetDocAndRecordChecklist '{PageSize}','{PageNo}'";
                var data = doc.GetAll(sql);                 
                return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}