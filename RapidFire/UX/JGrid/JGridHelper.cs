using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RapidFireLib.Lib.Core;
using RapidFireLib.Lib.Extension;
using RapidFireLib.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace RapidFireLib.UX.JGrid
{
    public class JGridHelper : ControllerBase
    {
        //DbContext context;
        Configuration Config = null;
        private Db db { get; set; }

        private Dynamic Dynamic => new Dynamic();

        public JGridHelper(IConfig config)
        {
            Config = new ConfigBuilder().Get(config);
            db = new Db(config);
        }

        public object Manage(JGridRequest packet, object filter, IQuerySelector querySelector, DbContext dbContext = null)
        {
            try
            {
                JGridPacket jGridPacket = new JGridPacket();
                dbContext = dbContext ?? Config.DB.DefaultDbContext;
                if (packet.Data != null)
                {
                    Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(packet.Data.ToString());

                    if (packet.RequestType == "ShowEdit" || packet.RequestType == "Update")
                    {
                        if (filter.GetPropertyValue("OverrideModel") != null && packet.IsOverride == true)
                        {
                            packet.ModelName = filterDictionary["OverrideModel"].ToString();
                        }


                    }
                }


                if (packet.RequestType == "Show" || packet.RequestType == "Export")
                {
                    if (!packet.IsOverride)
                    {
                        packet.ModelName = packet.ModelName.Contains("View") == true ? packet.ModelName : packet.ModelName + "View";
                    }
                }
                object model = null;
                if (packet.RequestType != "Export")
                {
                     model = Dynamic.GetInstance(Dynamic.GetFullyQualifiedPath(packet.ModelName, "Domain"));
                }
                switch (packet.RequestType)
                {
                    case "Show":
                        jGridPacket.Record = db.GetAll(model, querySelector.Select(packet), true, dbContext);
                        break;
                    case "Insert":
                        
                        object data = JsonConvert.DeserializeObject(packet.Data, model.GetType());
                        jGridPacket.Record = db.Save(data, dbContext).Result.Model;
                        db.Commit();
                        break;
                    case "Update":
                        Dictionary<string, object> requestData = JsonConvert.DeserializeObject<Dictionary<string, object>>(packet.Data);
                        requestData.Add("ModifiedBy", Config.APP.UserId);
                        requestData.Add("ModificationDate", DateTime.Now.ToString());
                        packet.Data = JsonConvert.SerializeObject(requestData);
                        jGridPacket.Record = Update(packet.ModelName, packet, dbContext);
                        db.Commit();

                        break;
                    case "Delete":
                        jGridPacket.Record = Delete(packet.ModelName, packet, dbContext);
                        InsertVerificationLog(packet, jGridPacket.Record, OperationType.DELETE, dbContext);
                        db.Commit();
                        break;
                    case "VerifyInfo":
                        jGridPacket.Record = GetVerifyInfo(model,packet, dbContext);
                        break;
                    case "Verify":
                        Dictionary<string, object> verifyData = JsonConvert.DeserializeObject<Dictionary<string, object>>(packet.Data);
                        verifyData.Add("VerifiedBy", Config.APP.UserId);
                        verifyData.Add("VerificationDate", DateTime.Now);
                        packet.Data = JsonConvert.SerializeObject(verifyData);
                        jGridPacket.Record = Update(packet.ModelName, packet, dbContext);
                        InsertVerificationLog(packet, jGridPacket.Record, OperationType.UPDATE, dbContext);
                        db.Commit();
                        return new { success = true, Data = "Verification Successful", VerifierName = Config.APP.UserName, VerificationDate = DateTime.Now };
                    case "Export":
                        return Export(packet, querySelector, dbContext);
                    case "ShowEdit":
                        object showData = db.GetById(model, packet.RecordId, false,dbContext);
                        string viewName = packet.ControllerName != "QB" ? packet.ModelName : packet.ControllerName;
                        //string html = Cast.View.GetString(packet.ControllerName + "/" + viewName+".cshtml");
                        var html = Cast.View.GetString("~/Views/" + packet.ControllerName + "/" + packet.ModelName + ".cshtml");
                        //string html = Tools.RenderView(packet.ControllerName + "/" + viewName);
                        
                        return new { success = true, json=showData, html };
                    case "ShowCreate":
                        html = Cast.View.GetString("~/Views/" + packet.ControllerName + "/" + packet.ModelName + ".cshtml");
                        return new { success = true, html };
                    case "Map":
                        List<MapJson> lst = new List<MapJson>
                        {
                            new MapJson() { Id = 4, Location = "GMP 1 (1-Dec-2018)", Latitude = "23.7491206", Longitude = "90.3602757" },
                            new MapJson() { Id = 5, Location = "GMP 2 (1-Jan-2019)", Latitude = "23.7478959", Longitude = "90.3629236" },
                            new MapJson() { Id = 3, Location = "GMP 3 (1-Feb-2019)", Latitude = "23.7489088", Longitude = "90.3649791" },
                            new MapJson() { Id = 2, Location = "GMP 4 (1-Mar-2019)", Latitude = "23.7477852", Longitude = "90.3629741" },
                            new MapJson() { Id = 1, Location = "GMP 5 (1-Apr-2019)", Latitude = "23.7390734", Longitude = "90.3609762" }
                        };
                        return new { success = true, Data = lst };
                }
                return new { Data = jGridPacket, success = true };
            }
            catch (Exception ex)
            {
                return new { success = false, Data = ex.Message };
            }
        }

        public object Export(JGridRequest packet, IQuerySelector querySelector, DbContext dbContext = null)
        {
            var httpContext = new Http().HttpContext;
            if (packet.Extension == null || packet.Extension == "" || packet.Extension != "csv")
            {
                packet.Extension = "xlsx";
            }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = db.GetDataTable(querySelector.Select(packet));
            //dt = db.GetDataTable(querySelector.Select(packet), dbContext);
            if (packet.Extension == "csv")
            {
                StringBuilder sb = new StringBuilder();
                IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                sb.AppendLine(string.Join(",", columnNames));
                foreach (DataRow row in dt.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                    sb.AppendLine(string.Join(",", fields));
                }
                string st = sb.ToString();
                return null;
            }
            else
            {
                dt.TableName = packet.ModelName;
                ds.Tables.Add(dt);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ds);
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Style.Font.Bold = true;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", packet.ModelName+ "."+packet.Extension);
                    }
                }
            }
        }

        public enum OperationType
        {
            UPDATE = 1,
            DELETE = 2
        }

        public void InsertVerificationLog(JGridRequest packet, object model, OperationType operationType, DbContext dbContext = null)
        {
            if (model.GetType().IsGenericType)
                foreach (var item in (IList)model)
                    PerformVerificationLog(packet, item, operationType, dbContext);
            else PerformVerificationLog(packet, model, operationType, dbContext);
        }
        private DbResultAfter PerformVerificationLog(JGridRequest packet, object model, OperationType operationType, DbContext dbContext = null)
        {
            System.Reflection.PropertyInfo pk = db.GetPrimaryKeyInfo(model);
            object pkValue = model.GetPropertyValue(pk.Name);
            object uUID = model.GetPropertyValue("UUID");
            List<string> childTableList = db.GetChildTableList(model, dbContext);
            DataVerificationLog dataVerificationLog = new DataVerificationLog
            {
                TableName = packet.ModelName,
                OperationType = (int)operationType,
                PrimaryKey = pk.Name,
                UUID = uUID == null ? "" : (string)uUID,
                IsClientUpdated = 0,
                RecordType = packet.RecordType == "Master" ? 1 : 2,
                ChildTables = childTableList.Count() == 0 ? "" : string.Join(",", childTableList)
            };
            dataVerificationLog.SetPropertyValue("Id", pkValue);
            dataVerificationLog.SetPropertyValue("NotifiedTo", model.GetPropertyValue("UserId")??0);
            dataVerificationLog.SetPropertyValue("NotifiedBy", Config.APP.UserId);
            return db.Save(dataVerificationLog, dbContext);
        }
        public object Update(string modelName, JGridRequest packet, DbContext dbContext = null)
        {

            object model = Dynamic.GetInstance(Dynamic.GetFullyQualifiedPath(modelName));
            Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(packet.Data.ToString());
            string[] ids = packet.RecordId.Split(',');
            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(model.GetType()));
            var pkInfo = db.GetPrimaryKeyInfo(model);
            foreach (string recordId in ids)
            {
                dictionary[pkInfo.Name] = recordId;
                object record = db.GetById(model, recordId);
                record = dictionary.DictionaryToModel(record);
                Dynamic.InvokeMethod("Add", list, model.GetType(), new Type[] { model.GetType() }, record);
            }
            object result = db.Update(list, dbContext).Model;
            return result;
        }

        public object Delete(string modelName, JGridRequest packet, DbContext dbContext = null)
        {
            
            object model = Dynamic.GetInstance(Dynamic.GetFullyQualifiedPath(modelName));
            model.SetPropertyValue(db.GetPrimaryKeyInfo(model).Name, packet.RecordId);
            object result = db.Delete(model, dbContext).Model;
            return result;
        }

        public object GetVerifyInfo(object model,JGridRequest packet, DbContext dbContext = null)
        {
            string pkName = db.GetPrimaryKeyInfo(model).Name;
            string sql = string.Format(@"SELECT ISNULL(ud.FullName, '') as UserName,
                                        d.EntryDate as DataCollectionDate
                                        ,ISNULL(d.IsVerified,0) as IsVerified
                                        ,ISNULL(u.FullName,'') as VerifierName
                                        ,d.VerificationDate as VerificationDate
                                        ,ISNULL(d.VerificationNote,'') as VerificationNote
                                        FROM {0} d
                                        LEFT JOIN AspNetUsers u on u.UserId = d.VerifiedBy
                                        LEFT JOIN AspNetUsers ud on u.UserId = d.UserId
                                        WHERE d.{1} = '{2}'", model.GetType().Name, pkName,packet.RecordId);
            var result = db.GetAll(new VerificationInfo(), sql, true, dbContext);
            var isGreaterThan = (int)result.GetPropertyValue("Count") != 0;
            if (!isGreaterThan)
                return null;
            result = ((IList)result)[0];
            return result;
        }

        public string RenderRazorViewToString(string viewName)
        {
            //using (var sw = new StringWriter())
            //{
            //    var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
            //    var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewPath, TempData, sw);
            //    viewResult.View.Render(viewContext, sw);
            //    viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
            //    return sw.GetStringBuilder().ToString();
            //}
            return "";
        }




    }

    public class JGridPacket
    {
        public object Record { get; set; }
    }

    public class JGridRequest
    {
        public string ControllerName { get; set; }
        public string ModelName { get; set; }
        public string RequestType { get; set; }
        public string RecordType { get; set; }
        public string RecordId { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public string Data { get; set; }
        public bool IsOverride { get; set; }
        public string Extension { get; set; }
    }

    public class MapJson
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }
}



//var model = Dynamic.GetInstance(Dynamic.GetFullyQualifiedPath(modelName));
//var pk = db.GetPrimaryKeyInfo(model).Name;
//var repository = Dynamic.GetInstance(typeof(Repository<>), model.GetType(), context);
//var record = ((IQueryable<object>)Dynamic.InvokeMethod("GetAll", repository, model.GetType())).Where($"{pk} = {keyValue}");

//private object DeleteRecord(object modelName)
//{
//    //var model = Dynamic.GetInstance(Dynamic.GetFullyQualifiedPath(modelName));
//    //var targetClass = Dynamic.GetInstance(typeof(Repository<>), model.GetType(), context);
//    //var obj = ((IQueryable<object>)Dynamic.InvokeMethod("GetAll", targetClass, model.GetType())).Where($"{pkName} = {keyValue}");
//    //obj = obj.Include(childitem);
//    //var f=(IEnumerable<object>)obj.GetPropertyValue(childListItem);
//    //Db.Delete(obj);

//    //var childs = (IEnumerable<object>)GetPropertyValue(modelName, pkName);
//    var c = modelName;
//    return c;
//}


//private void GetWithChild<T>()
//{
//    var childList = Db.GetChildTableList<T>();
//    var query = unitOfWork.Repositories<T>().GetAll().Where($"{pk.Name} = {packet.RecordId}");
//    //Child Include Start
//    foreach (var childitem in childList.ToList())
//    {
//        query = query.Include(childitem);
//    }
//    //Child Include End
//    var existingMasterWithChild = query.FirstOrDefault();
//}
