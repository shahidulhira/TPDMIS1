using BDCO.Domain.Aggregates;
using BDCO.Domain.Identity;
using BDCO.Domain.Models;
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
using System.Web.Http;

namespace BDCO.Web.Controllers
{
    public class FetchAPIController : ApiController
    {
        [HttpPost]
        [Route("api/Fetch/GetPacket")]
        public ApiResponse<Object> GetPacketByUserId(ApiPacketRequest apr)
        {
            ApiResponse<Object> returnObject = new ApiResponse<Object>();
            List<object> obj = new List<object>();
            switch (apr.TableName)
            {
                case "Login":
                    {
                        UsersLogin query = JsonConvert.DeserializeObject<UsersLogin>(apr.ApiPacket.Packet.ToString());
                        var result = new AspNetUsers().Login(query);
                        ApiPacket<LoginResponse> fp = new ApiPacket<LoginResponse>() { Packet = new LoginResponse() };
                        if (result != null)
                        {
                            returnObject.ApiPacket.Packet = new LoginResponse() { UserInfo = result.UserInfo/*, BlockInfo = result.BlockInfo ServicePoint = result.ServicePoint, GeoLocation = result.GeoLocation*/ };
                        }
                        ApiResponse<LoginResponse> response = new ApiResponse<LoginResponse>()
                        {
                            Success = result != null ? true : false,
                            ApiPacket = fp,
                            PageNo = apr.PageNo,
                            PageSize = apr.PageSize,
                            TotalRecord = 0,
                            Message = result != null ? "Login successfully." : "Invalid Username or Password",
                            Status = result != null ? 200 : 400
                        };
                        Tools.CopyClass(returnObject, response);
                        break;
                        //return returnObject;
                    }
                case "UserGeo":
                    {
                        var result = new PermittedGeoLocation().GetPermittedGeoLocation(new PermittedGeoLocationRM { UserId = apr.UserId }); ;


                        ApiPacket<PermittedGeoLocationViewModels> fp = new ApiPacket<PermittedGeoLocationViewModels>() { Packet = new PermittedGeoLocationViewModels() };
                        if (result != null)
                        {
                            returnObject.ApiPacket.Packet = new PermittedGeoLocationViewModels() { District = result.District, Upazila = result.Upazila, Unions = result.Unions, Village = result.Village, CenterInfo = result.CenterInfo, CampInfo = result.CampInfo };
                        }

                        ApiResponse<PermittedGeoLocationViewModels> response = new ApiResponse<PermittedGeoLocationViewModels>()
                        {
                            Success = result != null ? true : false,
                            ApiPacket = fp,
                            PageNo = apr.PageNo,
                            PageSize = apr.PageSize,
                            TotalRecord = 0,
                            Message = result != null ? "Record Fetched successfully." : "Record Fetched fail.",
                            Status = result != null ? 200 : 400
                        };
                        Tools.CopyClass(returnObject, response);
                        break;
                    }                
                case "UniqueId":
                    {
                        var requestObject = JsonConvert.DeserializeObject<RequestForUniqueId>(apr.ApiPacket.Packet.ToString());
                        var result = new UniqueId().SaveAndGet(requestObject ?? new RequestForUniqueId(), apr.UserId);
                        returnObject = GetApiResponse(apr, result.ToList<object>(), result.Count);
                        break;
                    }
                case "SpinnerData":
                    {
                        var spinnerRequest = JsonConvert.DeserializeObject<SpinnerRequest>(apr.ApiPacket.Packet.ToString());
                        SpinnerData spinner = new SpinnerData();
                        var result = spinner.APIGetSpinnerData(QueryFromAPIPacketRequest(apr), spinnerRequest);
                        obj.AddRange(result);
                        returnObject = GetApiResponse(apr, obj, apr.TotalRecord != 0 ? spinner.APIGetTotalRecord(QueryFromAPIPacketRequest(apr), spinnerRequest) : apr.TotalRecord);
                        break;
                    }
                case "BlockInfo":
                    {
                        var requestObject = new ForApiResponse() { UserId = apr.UserId };
                        BlockInfo blockinfo = new BlockInfo();
                        var result = blockinfo.APIGetBlockList(requestObject);
                        obj.AddRange(result);
                        returnObject = GetApiResponse(apr, result.ToList<object>(), result.Count);
                        break;
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
