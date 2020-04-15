using BDCO.Domain;
using BDCO.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDCO.Web.Controllers
{
    public class CompetencyTestController : Controller
    {
        // GET: CompetencyTest
        readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            string sql = $"EXEC [spGetCompetencyTestResult]";
            var result = _unitOfWork.GenericRepositories<CompetencyTestResultsVw>().GetRecordSet(sql).ToList();
            return View(result);
        }

        [HttpPost]
        public JsonResult GetCompetencyTestForTeachers(int? PageSize, int? PageNo)
        {
            try
            {
                string sql = $"EXEC GetCompetencyTestForTeachers '{PageSize}','{PageNo}'";
                var data = _unitOfWork.GenericRepositories<CompetencyTestView>().GetRecordSet(sql).ToList();
                return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Update(CompetencyUpdate competency)
        {
            try
            {
                //var uid = User.CurrentUserID();

                string result;
                CompetencyTest test = new CompetencyTest();
                if (test.Update(competency))
                {
                    result = "Successfully Updated";
                    return Json(new { success = true, Data = result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    result = "Sorry..!!";
                    return Json(new { success = true, Data = result }, JsonRequestBehavior.AllowGet); 
                }
            }
            catch(Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}