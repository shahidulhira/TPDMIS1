using BDCO.Domain.Aggregates;
using BDCO.Domain.Identity;
using BDCO.Domain.Models.Systems;
using BDCO.Domain.Query;
using BDCO.Domain.RequestModels;
using BDCO.Domain.RequestParams;
using BDCO.Domain.Utility;
using BDCO.Web.Models;
using BDCO.Web.Models.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static BDCO.Web.WebBase;

namespace BDCO.Web.Controllers.API
{
    public class NewFetchAPIController : ApiController
    {
        [HttpPost]
        [Route("api/NewFetch/GetPacket")]
        public ApiResponse<Object> GetPacketByUserId(ApiPacketRequest apr)
        {
            ApiResponse<Object> returnObject = new ApiResponse<Object>();
            List<object> obj = new List<object>();
            switch (apr.Command)
            {
                case "GetData":
                    {
                        if(apr.TableName == "SpinnerData")
                        {
                            var spinnerRequest = JsonConvert.DeserializeObject<SpinnerRequest>(apr.ApiPacket.Packet.ToString());
                            SpinnerData spinner = new SpinnerData();
                            var result = spinner.APIGetSpinnerData(QueryFromAPIPacketRequest(apr), spinnerRequest);
                            obj.AddRange(result);
                            returnObject = GetApiResponse(apr, obj, apr.TotalRecord != 0 ? spinner.APIGetTotalRecord(QueryFromAPIPacketRequest(apr), spinnerRequest) : apr.TotalRecord);
                            break;
                        }
                        else
                        {
                            var dynamicModelType = Dynamic.GetObjectType(apr.TableName);
                            var selectObj = Activator.CreateInstance(dynamicModelType);
                            var data = JsonConvert.DeserializeObject<GetDataPacket>(apr.ApiPacket.Packet.ToString()); 
                            var result = Dynamic.DynamicSelectWithPaging(selectObj, data, apr.PageNo, apr.PageSize);
                            obj.AddRange(result);
                            returnObject = GetApiResponse(apr, obj, apr.TotalRecord != 0 ? Dynamic.DynamicSelectTotalRecortCount(selectObj, data) : apr.TotalRecord);
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
                            Message = "Table is not registered to System",
                            Status = 400
                        };
                        return response;
                    }
            }
            return returnObject;

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
        private ApiResponse<object> GetApiResponse(ApiPacketRequest apr, List<object> list, long totalRecord)
        {
            ApiResponse<object> returnObject = new ApiResponse<object>();
            ApiPacket<object> fp = new ApiPacket<object>() { PacketList = list };
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
