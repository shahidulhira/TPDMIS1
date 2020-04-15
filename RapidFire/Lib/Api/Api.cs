using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidFireLib.Lib.Api
{
    public class WebApi
    {
        Db db = null;
        IConfig _config;
        Configuration Config = null;

        public WebApi(IConfig config)
        {
            _config = config;
            Config = new ConfigBuilder().Get(config);
            db = new Db(config);
        }

        public enum ProcessType
        {
            Add = 0,
            Update,
            Delete,
            Get
        }
        public class SqlPacket
        {
            public string ModelName { get; set; }
            public string Select { get; set; }
            public string Where { get; set; }
            public bool IncludeChild { get; set; }
            public string OrderBy { get; set; }
        }
        Dynamic dynamic = new Dynamic();
        

        string[] operation = new string[] { "add", "update", "delete", "getdata" };
        private Result ProcessOperation(object modelData, DbContext dbContext, object expressionOrSql)
        {
            if (expressionOrSql == null)
                return db.Save(modelData, dbContext).Result;
            if (expressionOrSql.GetType() == typeof(String))
                return db.Save(modelData, dbContext, (string)expressionOrSql).Result;
            else return db.Save(modelData, dbContext, (MulticastDelegate)expressionOrSql).Result;
        }
        public ApiResponse ProcessSync(ApiPacketRequest apr, DbContext dbContext = null, object expressionOrSql = null,
            params IApiHandler[] apiHandlers)
        {
            dbContext = dbContext ?? Config.DB.DefaultDbContext;
            object model = dynamic.GetInstance(dynamic.GetFullyQualifiedPath(apr.TableName));
            object modelData = JsonConvert.DeserializeObject(apr.ApiPacket.Packet.ToString(), model.GetType());

            if (apiHandlers != null)
                for (int i = 0; i < apiHandlers.Count(); i++)
                    modelData = apiHandlers[i].Handle(Mode.Pre, (ProcessType)Array.IndexOf(operation, apr.Command), modelData,
                        db);

            try
            {
                ApiResponse apiResponse = new ApiResponse()
                { Success = false, Message = "Table is not registered", Status = 400 };
                ;
                if (apr.Command.ToUpper().Equals("ADD"))
                    apiResponse = ResultToApiResponse(ProcessOperation(modelData, dbContext, expressionOrSql));
                else if (apr.Command.ToUpper().Equals("UPDATE"))
                    apiResponse = ResultToApiResponse(ProcessOperation(modelData, dbContext, expressionOrSql));
                else if (apr.Command.ToUpper().Equals("DELETE"))
                    apiResponse = ResultToApiResponse(db.Delete(modelData, dbContext));
                //Error Log To a File
                db.Commit();
                if (apiHandlers != null)
                    for (int i = 0; i < apiHandlers.Count(); i++)
                        modelData = apiHandlers[i].Handle(Mode.Post, (ProcessType)Array.IndexOf(operation, apr.Command), model,
                            db);
                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Success = false,
                    Message = ex.Message,
                    Status = 400
                };
            }
        }

        private ApiResponse ResultToApiResponse(Result result)
        {
            var primaryKeyValue = db.GetPrimaryKeyValue(result.Model);
            return new ApiResponse()
            { Success = result.Success, Message = result.Message, RecordId = primaryKeyValue, Status = result.Success ? 200 : 400 };
        }

        public Task<ApiResponse<object>> ProcessFetchAsync(ApiPacketRequest apr, DbContext dbContext = null,
            params IApiHandler[] attachApiHandler)
        {
            return Task.Run(() => ProcessFetch(apr, dbContext, attachApiHandler));
        }

        public ApiResponse<object> ProcessFetch(ApiPacketRequest apr, DbContext dbContext = null,
            params IApiHandler[] attachApiHandler)
        {
            //declartation part
            ApiResponse<Object> returnObject;
            List<object> obj = new List<object>();
            bool ispageSizeOrPageNoZeo = false;


            object model = dynamic.GetInstance(dynamic.GetFullyQualifiedPath(apr.TableName));
            object modelData = JsonConvert.DeserializeObject(apr.ApiPacket.Packet.ToString(), model.GetType());


            //Page Size & Page No 0 
            ispageSizeOrPageNoZeo = apr.PageNo == 0 && apr.PageSize == 0;
            apr.PageNo = apr.PageNo == 0 ? 1 : apr.PageNo;
            apr.PageSize = apr.PageSize == 0 ? int.MaxValue : apr.PageSize;

            List<object> result2 = new List<object>();


            switch (apr.Command)
            {
                case "GetData":
                    {
                        if (apr.TableName == "SpinnerData")
                        {
                            var spinnerRequest = JsonConvert.DeserializeObject<SpinnerRequest>(apr.ApiPacket.Packet.ToString());
                            SpinnerData spinner = new SpinnerData(_config);
                            List<SpinnerValue> result = new List<SpinnerValue>();
                            Tuple<int, List<SpinnerValue>> tupleResult = new Tuple<int, List<SpinnerValue>>(0, new List<SpinnerValue>());
                            if (!string.IsNullOrEmpty(spinnerRequest.Sql))
                            {
                                tupleResult = spinner.APIGetSpinnerDataFromSP(spinnerRequest.Sql, spinnerRequest.DisplayText, spinnerRequest.ValueText, apr.PageSize, apr.PageNo);
                                result = tupleResult.Item2;
                            }
                            else
                            {
                                result = spinner.APIGetSpinnerData(QueryFromAPIPacketRequest(apr), spinnerRequest);
                            }
                            obj.AddRange(result);

                            returnObject = GetApiResponse(apr, obj,
                                tupleResult.Item1 == 0 ? result.Count() : tupleResult.Item1);
                            break;
                        }
                        else if (apr.TableName == "SingleValue")
                        {
                            var spinnerRequest = JsonConvert.DeserializeObject<SqlPacket>(apr.ApiPacket.Packet.ToString());
                            var singleValue = db.GetSingleValue(spinnerRequest.Select, spinnerRequest.Where,
                                spinnerRequest.OrderBy);
                            obj.Add(singleValue);
                            returnObject = GetApiResponse(apr, obj, 1);
                            break;
                        }
                        else
                        {
                            var packet = JsonConvert.DeserializeObject<SqlPacket>(apr.ApiPacket.Packet.ToString());

                            var select = packet.Select;
                            var where = packet.Where;
                            var orderBy = packet.OrderBy;
                            var includedChild = packet.IncludeChild;
                            model = dynamic.GetInstance(dynamic.GetFullyQualifiedPath(packet.ModelName));

                            var result = db.SelectWithPaging(model, select, where, orderBy, includedChild, apr.PageNo,
                                apr.PageSize, dbContext).ToList();

                            object handlerResult = result;
                            if (attachApiHandler != null)
                                for (int i = 0; i < attachApiHandler.Count(); i++)
                                    handlerResult = attachApiHandler[i].Handle(Mode.Pre,
                                        ProcessType.Get, handlerResult, db);

                            if (handlerResult.GetType().GenericTypeArguments.Length == 0)
                            {
                                if (ispageSizeOrPageNoZeo)
                                {
                                    apr.PageNo = 0;
                                    apr.PageSize = 0;
                                }
                                returnObject = GetApiResponseForPacket(apr, handlerResult, apr.TotalRecord != 0 ? db.SelectTotalRecordCount(
                                    model,
                                packet.Select, packet.Where, packet.OrderBy,
                                dbContext) : apr.TotalRecord);
                            }
                            else
                            {
                                obj.AddRange((List<object>)handlerResult);
                                if (ispageSizeOrPageNoZeo)
                                {
                                    apr.PageNo = 0;
                                    apr.PageSize = 0;
                                }

                                returnObject = GetApiResponse(apr, obj,
                                    apr.TotalRecord != 0
                                        ? db.SelectTotalRecordCount(model, packet.Select, packet.Where, packet.OrderBy,
                                            dbContext)
                                        : apr.TotalRecord);
                            }

                            break;
                        }
                    }
                default:
                    {
                        ApiResponse<object> response = new ApiResponse<object>()
                        {
                            Success = false,
                            ApiPacket = null,
                            PageNo = apr.PageNo,
                            PageSize = apr.PageSize,
                            TotalRecord = 0,
                            Message = "Table is not registered",
                            Status = 400
                        };
                        return response;
                    }
            }

            if (attachApiHandler != null)
            {
                for (int i = 0; i < attachApiHandler.Count(); i++)
                    attachApiHandler[i].Handle(Mode.Post, ProcessType.Get, returnObject.ApiPacket, db);
            }
            if (returnObject.ApiPacket.Packet != null)
            {
                returnObject.ApiPacket.PacketList = null;
            }
            return returnObject;
        }

        public ApiResponse<Object> GetPacketByUserId(ApiPacketRequest apr)
        {
            return null;
        }

        private ForApiResponse QueryFromAPIPacketRequest(ApiPacketRequest apr)
        {
            return new ForApiResponse()
            {
                PageNo = apr.PageNo,
                PageSize = apr.PageSize,
                UserId = apr.UserId
            };
        }

        private ApiResponse<object> GetApiResponseForPacket(ApiPacketRequest apr, object packet, long totalRecord)
        {
            ApiResponse<object> returnObject = new ApiResponse<object>();
            //ApiPacket<object> fp = new ApiPacket<object>() {PacketList = list.ToList()};
            ApiResponse<object> response = new ApiResponse<object>()
            {
                Success = packet != null ? true : false,
                ApiPacket = new ApiPacket<object>()
                {
                    Packet = packet
                },
                PageNo = apr.PageNo,
                PageSize = apr.PageSize,
                TotalRecord = totalRecord,
                Message = packet != null ? "Record Fetched successfully." : "Record Fetched fail.",
                Status = packet != null ? 200 : 400
            };
            Tools.CopyClass(returnObject, response);
            returnObject.ApiPacket.PacketList = new List<object>();
            returnObject.ApiPacket.Packet = packet;
            return returnObject;
        }

        private ApiResponse<object> GetApiResponse(ApiPacketRequest apr, IEnumerable<object> list, long totalRecord)
        {
            ApiResponse<object> returnObject = new ApiResponse<object>();
            ApiPacket<object> fp = new ApiPacket<object>() { PacketList = list.ToList() };
            ApiResponse<object> response = new ApiResponse<object>()
            {
                Success = list != null ? true : false,
                ApiPacket = fp,
                PageNo = apr.PageNo,
                PageSize = apr.PageSize,
                TotalRecord = totalRecord,
                Message = list != null ? "Record Fetched successfully." : "Record Fetched fail.",
                Status = list != null ? 200 : 400
            };
            Tools.CopyClass(returnObject.ApiPacket, fp);
            Tools.CopyClass(returnObject, response);
            returnObject.ApiPacket.PacketList = new List<object>();
            returnObject.ApiPacket.PacketList.AddRange(list);
            return returnObject;
        }
    }
}

// proper return + command result ?? all 400 ?