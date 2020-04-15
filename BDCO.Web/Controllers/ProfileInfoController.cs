using BDCO.Core.Command;
using BDCO.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDCO.Web.Controllers
{
    public class ProfileInfoController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }    
        
        public ActionResult Details()
        {
            return View();
        }

        public JsonResult Save(ProfileInfo profile)
        {
            try
            {
                CommandResult result = profile.Save(profile);
                return Json(new { success = true, Data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllProfileInfo(ProfileInfoFilter filter)
        {
            return Json(new ProfileInfo().GetAllProfileInfo(filter), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProfileInfoByID(int id)
        {
            return Json(new ProfileInfo().GetProfileInfoByID(id), JsonRequestBehavior.AllowGet);
        }


        
        public JsonResult Delete(ProfileInfo profile)
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