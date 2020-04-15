using BDCO.Core.Command;
using BDCO.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDCO.Web.Controllers
{
    public class LearningFacilityController : Controller
    {
        // GET: LCProfile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        public JsonResult Save(LearningFacility teacherProfile)
        {
            try
            {
                CommandResult result = teacherProfile.Save(teacherProfile);
                return Json(new { success = true, Data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllLearningFacility(LearningFacilityFilter filter)
        {
            return Json(new LearningFacility().GetAllLearningFacility(filter), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLearningFacilityByID(int id)
        {
            return Json(new LearningFacility().GetLearningFacilityByID(id), JsonRequestBehavior.AllowGet);
        }



        public JsonResult Delete(LearningFacility profile)
        {
            try
            {
                CommandResult result = profile.Delete(profile);
                return Json(new { success = true, Data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}