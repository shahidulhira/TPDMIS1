using BDCO.Core.Command;
using BDCO.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDCO.Web.Controllers
{
    public class TrainingController : Controller
    {
        // GET: Training
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult GetAllTrainingInfo(ProfileInfoFilter filter)
        {
            return Json(new TrainingInfo().GetAllTrainingInfo(filter), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTrainingInfoByID(string id)
        {
            return Json(new TrainingInfo().GetTrainingInfoByID(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(TrainingInfo trainingInfo, List<string> trainingInfoTopic, List<string> trainingInfoMaterial)
        {
            try
            {
                TrainingInfoMaterial material = new TrainingInfoMaterial();
                TrainingInfoTopic topic = new TrainingInfoTopic();

                List<TrainingInfoMaterial> lstMaterial = new List<TrainingInfoMaterial>();
                List<TrainingInfoTopic> lstIopic = new List<TrainingInfoTopic>();

                foreach (var item in trainingInfoTopic)
                {
                    topic.TopicId = item;
                    topic.RowId = 0;
                    topic.TopicType = "1";
                    if (trainingInfo.TrainingId == null)
                        topic.TrainingId = "0";
                    else
                        topic.TrainingId = trainingInfo.TrainingId;
                    lstIopic.Add(topic);
                }

                foreach (var item in trainingInfoMaterial)
                {
                    material.MaterialId = item;
                    material.MaterialType = trainingInfo.CategoryId;
                    material.RowId = 0;

                    if (trainingInfo.TrainingId == null)
                        material.TrainingId = "0";
                    else
                        material.TrainingId = trainingInfo.TrainingId;

                    lstMaterial.Add(material);
                }

                //trainingInfo.trainingInfoMaterials = lstMaterial;
                //trainingInfo.trainingInfoTopic = lstIopic;

                CommandResult result = trainingInfo.Save(trainingInfo, lstMaterial, lstIopic);
                return Json(new { success = true, Data = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Delete(TrainingInfo profile)
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