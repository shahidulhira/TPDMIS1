using BDCO.Core.Command;
using BDCO.Domain;
using BDCO.Domain.Utility;
using BDCO.Web.Models.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using static BDCO.Web.WebBase;

namespace BDCO.Web.Controllers.API
{
    public class NewSyncAPIController : ApiController
    {
        [HttpPost]
        [Route("api/NewSync/SendPacket")]
        public ApiResponse SyncData(ApiPacketRequest apr)
        {
            ApiResponse returnObject = new ApiResponse();
            CommandResult commandResult = new CommandResult();
            UnitOfWork unitOfWork = new UnitOfWork();
            try
            {
                string result = "";
                switch (apr.Command)
                {
                    case "add":
                        {                              
                            var dynamicModelType = Dynamic.GetObjectType(apr.TableName);
                            var addObj = JsonConvert.DeserializeObject(apr.ApiPacket.Packet.ToString(), dynamicModelType);
                            Dynamic.DynamicRepo(addObj, unitOfWork.context, DBOperations.INSERT);
                            result = unitOfWork.SaveChange();
                            break;
                        }
                    case "update":
                        {
                            var dynamicModelType = Dynamic.GetObjectType(apr.TableName);
                            var updateObj = Activator.CreateInstance(dynamicModelType);
                            Tools.CopyJsonData(updateObj, apr.ApiPacket.Packet.ToString());
                            Dynamic.DynamicRepo(updateObj, unitOfWork.context, DBOperations.UPDATE);
                            result = unitOfWork.SaveChange();
                            break;
                        }
                    case "delete":
                        {
                            var dynamicModelType = Dynamic.GetObjectType(apr.TableName);
                            var updateObj = Activator.CreateInstance(dynamicModelType);
                            Tools.CopyJsonData(updateObj, apr.ApiPacket.Packet.ToString());
                            Dynamic.DynamicRepo(updateObj, unitOfWork.context, DBOperations.DELETE);
                            result = unitOfWork.SaveChange();
                            break;
                        }
                    default:
                        {
                            return new ApiResponse()
                            {
                                Success = false,
                                Message = "Table is not registered",
                                Status = 400
                            };
                        }
                }
                commandResult = new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : string.Format(@"Record {0} successfully.", apr.Command == "add" ? "saved" : apr.Command == "update" ? "updated" : apr.Command == "delete" ? "deleted" : "")
                };

                Tools.CopyClass(returnObject, commandResult);
                return returnObject;
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
    }
}
