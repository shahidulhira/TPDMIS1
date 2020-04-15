using BDCO.Core.Command;
using BDCO.Domain;
using BDCO.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDCO.Web.Controllers
{
    public class InterviewQuestionnaireController : Controller
    {
        // GET: InterviewQuestionnaire
        readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            string sql = $"EXEC spGetInterviewQuestionnaireResult";
            var result = _unitOfWork.GenericRepositories<InterviewQuestionnaireResults>().GetRecordSet(sql).ToList();
            return View(result);
        }
        [HttpPost]
        public JsonResult GetQuestionnaire(int PageSize, int PageNo)
        {
            try
            {
                InterviewQuestionnaire doc = new InterviewQuestionnaire();
                string sql = $"EXEC spInterviewQuestionnaire '{PageSize}','{PageNo}'";
                var data = doc.GetAll(sql);
                return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Questionnaire()
        {
            return View();
        }

        public JsonResult Save(InterviewQuestionnaire questionnaire)
        {
            try
            {
                CommandResult result = questionnaire.Save(questionnaire);
                return Json(new { success = true, Data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FilterAll(InterviewFilter filter)
        {
            return Json(new InterviewQuestionnaire().FilterAll(filter), JsonRequestBehavior.AllowGet);
        }
    }
}