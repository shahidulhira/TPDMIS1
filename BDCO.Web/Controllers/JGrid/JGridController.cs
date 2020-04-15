using BDCO.Domain;
using BDCO.Domain.Aggregates;
using BDCO.Domain.Utility;
using BDCO.Web.Utility.JGrid;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using ClosedXML.Excel;

namespace BDCO.Web.Controllers
{
    public class JGridController : Controller
    {
        UtilityHelper _utility = new UtilityHelper();
        UnitOfWork unitOfWork = new UnitOfWork();
        [Authorize]
        public ActionResult ManageGrid(JGridRequest packet)
        {
            try
            {
                JGridPacket p = new JGridPacket();
                FilterData filter = new FilterData();
                if (packet.Data != null)
                {
                    filter = JsonConvert.DeserializeObject<FilterData>(packet.Data.ToString());
                    if (packet.RequestType == "ShowEdit" || packet.RequestType == "Update")
                        if (filter.OverrideModel != null && packet.IsOverride == true)
                        {
                            packet.ModelName = filter.OverrideModel;
                        }
                }

                if (packet.RequestType == "Show")
                {
                    //packet.ModelName = packet.ModelName.Contains("View") == true ? packet.ModelName : packet.ModelName + "View";
                    if (!packet.IsOverride) packet.ModelName = packet.ModelName.Contains("View") == true ? packet.ModelName : packet.ModelName + "View";
                }

                Assembly[] asm = AppDomain.CurrentDomain.GetAssemblies().Where(x=>x.FullName.Contains("BDCO")).ToArray();
                Type item = null;
                for (int i = 0; i < asm.Length; i++)
                {
                    item = asm[i].GetTypes().Where(x => x.Name == packet.ModelName ).FirstOrDefault();
                    if (item != null)
                    {
                        break;
                    }
                }


                var jgridType = typeof(JGridHelper<>);
                var jgridObject = jgridType.MakeGenericType(item);
                var obj = Activator.CreateInstance(jgridObject);
                switch (packet.RequestType)
                {
                    case "Show":
                        p.Record = MethodInvoker("ShowList", obj, new object[] { new QuerySelector().getSelector(packet) });
                        break;
                    case "Insert":
                        p.Record = MethodInvoker("Insert", obj, new object[] { packet, asm, item });
                        break;
                    case "Update":
                        var requestDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(packet.Data);
                        requestDataDictionary.Add("ModifiedBy", User.CurrentUserID());
                        requestDataDictionary.Add("ModificationDate", DateTime.Now.ToString());
                        packet.Data = JsonConvert.SerializeObject(requestDataDictionary);
                        p.Record = MethodInvoker("EditDelete", obj, new object[] { 1, packet, asm, User.CurrentUserID() });
                        break;
                    case "Delete":
                        p.Record = MethodInvoker("EditDelete", obj, new object[] { 2, packet, asm, User.CurrentUserID() });
                        break;
                    case "VerifyInfo":
                        var pk = _utility.GetPrimaryKeyInfo(item);
                        var verifyInfo = new VerifyInfo();
                        jgridObject = jgridType.MakeGenericType(verifyInfo.GetType());
                        obj = Activator.CreateInstance(jgridObject);

                        var jsonVerifiyInfo = MethodInvoker("GetVerifyInfo", obj, new object[] { packet, pk.Name });
                        return Json(new { success = true, Data = jsonVerifiyInfo }, JsonRequestBehavior.AllowGet);
                    case "Verify":
                        var requestSourceDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(packet.Data);
                        if (requestSourceDictionary.ContainsKey("IsVerified"))
                            if (requestSourceDictionary.FirstOrDefault(x => x.Key == "IsVerified").Value.ToString() == "2") SendPushMessage();


                        var user = User.CurrentUserID() == 0 ? 0 : User.CurrentUserID();
                        var userName = User.DisplayName() == "" ? "" : User.DisplayName();
                        requestSourceDictionary.Add("VerifiedBy", user);
                        requestSourceDictionary.Add("VerificationDate", DateTime.Now);
                        packet.Data = JsonConvert.SerializeObject(requestSourceDictionary);
                        p.Record = MethodInvoker("EditDelete", obj, new object[] { 1, packet, asm, User.CurrentUserID() });
                        return Json(new { success = true, Data = "Verification Successful", VerifierName = userName, VerificationDate = DateTime.Now }, JsonRequestBehavior.AllowGet);
                    case "Export":
                        break;
                    case "ShowEdit":
                        //packet.ControllerName = "EsFollowUp";
                        var json = MethodInvoker("Show", obj, new object[] { packet });
                        string viewName = packet.ControllerName != "QB" ? packet.ModelName : packet.ControllerName;
                        var html = RenderRazorViewToString("~/Views/" + packet.ControllerName + "/" + viewName + ".cshtml").Trim();
                        return Json(new { success = true, json, html }, JsonRequestBehavior.AllowGet);
                    case "ShowCreate":
                        html = RenderRazorViewToString("~/Views/" + packet.ControllerName + "/" + packet.ModelName + ".cshtml").Trim();
                        return Json(new { success = true, html }, JsonRequestBehavior.AllowGet);
                    case "Map":
                        List<MapJson> lst = new List<MapJson>();
                        lst.Add(new MapJson() { Id = 4, Location = "GMP 1 (1-Dec-2018)", Latitude = "23.7491206", Longitude = "90.3602757" });
                        lst.Add(new MapJson() { Id = 5, Location = "GMP 2 (1-Jan-2019)", Latitude = "23.7478959", Longitude = "90.3629236" });
                        lst.Add(new MapJson() { Id = 3, Location = "GMP 3 (1-Feb-2019)", Latitude = "23.7489088", Longitude = "90.3649791" });
                        lst.Add(new MapJson() { Id = 2, Location = "GMP 4 (1-Mar-2019)", Latitude = "23.7477852", Longitude = "90.3629741" });
                        lst.Add(new MapJson() { Id = 1, Location = "GMP 5 (1-Apr-2019)", Latitude = "23.7390734", Longitude = "90.3609762" });
                        return Json(new { success = true, Data = lst }, JsonRequestBehavior.AllowGet);
                    case "Chart":
                        var jsonData = MethodInvoker("ChartData", obj, new object[] { new ChartSelector().getSelector(packet) });
                        return Json(new { success = true, Data = jsonData }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = true, Data = p }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public void Export(string filterData)
        {
            FilterData filter = new FilterData();
            filter = JsonConvert.DeserializeObject<FilterData>(filterData);

            if (filter.Extension == null || filter.Extension == "" || filter.Extension != "csv")
            {
                filter.Extension = "xlsx";
            }
            JGridRequest packet = new JGridRequest();
            packet.ModelName = filter.ModelName;
            packet.Data = filterData;
            Assembly[] asm = AppDomain.CurrentDomain.GetAssemblies();
            Type item = null;
            for (int i = 0; i < asm.Length; i++)
            {
                item = asm[i].GetTypes().Where(x => x.Name == packet.ModelName).FirstOrDefault();
                if (item != null)
                {
                    break;
                }
            }
            var jgridType = typeof(JGridHelper<>);
            var jgridObject = jgridType.MakeGenericType(item);
            var obj = Activator.CreateInstance(jgridObject);

            packet.ModelName = packet.ModelName;

            if (filter.Extension == "csv")
            {
                var st = MethodInvoker("ExportCSV", obj, new object[] { new ExportSelector().getSelector(packet) });
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + packet.ModelName + "." + filter.Extension);
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(st);
                Response.Flush();
                Response.End();
            }
            else
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = (DataTable)MethodInvoker("ExportExcel", obj, new object[] { new ExportSelector().getSelector(packet) });
                dt.TableName = packet.ModelName;
                ds.Tables.Add(dt);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ds);
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Style.Font.Bold = true;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + packet.ModelName + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }



        //public JsonResult SendNotification()
        //{
        //    try
        //    {
        //        string sql = string.Format(@"EXEC GetPushNotificationMessage {0}", User.CurrentUserID());
        //        var lstMessages = unitOfWork.context.Database.SqlQuery<PushVerificationView>(sql).ToList();
        //        WebBase.MessagingResult res = new WebBase.MessagingResult();
        //        foreach (var item in lstMessages)
        //        {
        //            item.ModelName = "PushVerification";
        //            var pushData = new PushVerification();
        //            Tools.CopyClass(pushData, item);
        //            //string token = "cUJjpC9WReA:APA91bGAwY7FK99GXr68NbF9SRxTsUKbgc0Jj7L8eDFhzH-b8j9FDtskgVVrqdFEkOFqVkySeJvwVuInOX1PfQUfPk4YoZc2oFpfH0xHKDVJ5mqHHdn8ohymgIGKtEhGF55jtbeI15Q_";
        //            res = WebBase.Messaging.SendPushMessage(WebBase.Config.serverKey, item.Token, pushData, WebBase.Config.senderId);
        //            if (res.Successful)
        //            {
        //                WebBase.Messaging.UpdateMessage(item.NotifiedTo, item.NotifiedBy);
        //            }
        //        }
        //        if (res.Successful)
        //            return Json(new { success = true, Data = "Notificaiton Send Successful" }, JsonRequestBehavior.AllowGet);
        //        else
        //        {
        //            var result = JsonConvert.SerializeObject(res.Error.Message, Formatting.Indented,
        //                   new JsonSerializerSettings
        //                   {
        //                       ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                   });
        //            return Json(new { success = false, Data = result }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }

        //}


        public string RenderRazorViewToString(string viewName)
        {
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        private void SendPushMessage()
        {
            string serverKey = "AAAAuYZIzbo:APA91bFDt1ekYu2n_HfpQNn1M69bdWJPSDL2o-84nLZELW3YKObVly-f9UzaFxYR_RCE2v7qRgtyrCTOM8G8V0IsRbonaxI-lzJ0tkdhHrJ36u-ETOtJdP6Tc1qSHOdUfpdiYczT1YnT";
            //string token = "e_0_AA9TYjY:APA91bEa0DtrvDmld1-H3D9aCvxjAcZJ7PmVAGWiiNv9RFoqQnSF7i-yqR4lkH68z8SCjd23EO_wq_hgoOtPGYOnrcNDMTTmiDQNs4p5J5db0XRulIeCs0sFcxpFpNhO57V8XfxmPbSy";
            string token = "eAWSl3e_NK0:APA91bHJPlj1HH5z7TotP8EO0C9K2Jm1s_wZ2P27tetR5VgozFsJ6lo2brMCr7sFOYb52wFrXNG56Wki5KnprUqR8Tjpb48B7i48Fpz4WpJZdBOMvi5y2tdeV2OpLtXLMmNhtl0jO_gD";

            FCMPushNotification f = new FCMPushNotification();
            f.SendNotification("Varification Note", "You have 3 IGA record modified. Go to application and cehck.", "toic", token);
        }
        private object MethodInvoker(string methodName, object obj, params object[] parameters)
        {
            Type t = obj.GetType();
            MethodInfo method = t.GetMethod(methodName);
            var returnValue = method.Invoke(obj, parameters);
            return returnValue;
        }

        public class JGridPacket
        {
            public object Record { get; set; }
        }

        public class MapJson
        {
            public int Id { get; set; }
            public string Location { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }

        }

        public class VerifyInfo
        {
            public int? IsVerified { get; set; }
            public string UserName { get; set; }
            public DateTime? DataCollectionDate { get; set; }
            public string VerifierName { get; set; }
            public DateTime? VerificationDate { get; set; }
            public string VerificationNote { get; set; }
        }
    }
}