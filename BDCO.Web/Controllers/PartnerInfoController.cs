using BDCO.Core.Command;
using BDCO.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDCO.Web.Controllers
{
    public class PartnerInfoController : Controller
    {
        // GET: PartnerProfile
        public ActionResult Index()
        {
            return View();
        } 
        
        public ActionResult Details()
        {
            return View();
        }

        public JsonResult Save(PartnerInfo teacherProfile)
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

        public ActionResult GetAllPartnerInfo(PartnerInfoFilter filter)
        {
            return Json(new PartnerInfo().GetAllPartnerInfo(filter), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPartnerInfoByID(int id)
        {
            return Json(new PartnerInfo().GetPartnerInfoByID(id), JsonRequestBehavior.AllowGet);
        }



        public JsonResult Delete(PartnerInfo profile)
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