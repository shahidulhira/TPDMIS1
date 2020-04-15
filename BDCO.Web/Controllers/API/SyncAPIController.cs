using BDCO.Core.Command;
using BDCO.Domain.Aggregates;
using BDCO.Domain.Utility;
using BDCO.Web.Models.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BDCO.Web.Controllers
{
    public class SyncAPIController : ApiController
    {

        [HttpPost]
        [Route("api/Sync/SendPacket")]
        public ApiResponse SyncData(ApiPacketRequest apr)
        {
            ApiResponse returnObject = new ApiResponse();
            CommandResult commandResult = new CommandResult();
            try
            {
                /*
                switch (apr.TableName)
                {
                    case "MemberInfo":
                        {
                            MemberInfo command = new MemberInfo();
                            var memberInfo = JsonConvert.DeserializeObject<MemberInfo>(apr.ApiPacket.Packet.ToString());
                            commandResult = command.SaveOrUpdate(memberInfo);
                            break;
                        }
                    //case "Admission":
                    //    {
                    //        Admission command = new Admission();
                    //        var admisson = JsonConvert.DeserializeObject<Admission>(apr.ApiPacket.Packet.ToString());
                    //        commandResult = command.SaveOrUpdate(admisson);
                    //        break;
                    //    }
                    case "HomeVisit":
                        {
                            HomeVisit command = new HomeVisit();
                            var homeVisit = JsonConvert.DeserializeObject<HomeVisit>(apr.ApiPacket.Packet.ToString());
                            commandResult = command.SaveOrUpdate(homeVisit);
                            break;
                        }
                    case "Distribution":
                        {
                            Distribution command = new Distribution();
                            var distribution = JsonConvert.DeserializeObject<Distribution>(apr.ApiPacket.Packet.ToString());
                            commandResult = command.SaveOrUpdate(distribution);
                            break;
                        }
                    case "SessionInfo":
                        {
                            SessionInfo command = new SessionInfo();
                            command = JsonConvert.DeserializeObject<SessionInfo>(apr.ApiPacket.Packet.ToString());
                            commandResult = command.Save(command);
                            break;
                        }
                    case "Gmp":
                        {
                            Gmp command = new Gmp();
                            command = JsonConvert.DeserializeObject<Gmp>(apr.ApiPacket.Packet.ToString());
                            commandResult = command.Save(command);
                            break;
                        }
                    case "UnUsedUIdAndHHId":
                        {
                            UnusedUId command = new UnusedUId();
                            var obj = JsonConvert.DeserializeObject<UnusedUIdAndHHIdViewModel>(apr.ApiPacket.Packet.ToString());
                            commandResult = command.SaveUIdAndHHId(obj);
                            break;
                        }
                    case "UniqueId":
                        {
                            UniqueId command = new UniqueId();
                            var obj = JsonConvert.DeserializeObject<List<UniqueId>>(JsonConvert.SerializeObject(apr.ApiPacket.PacketList));
                            commandResult = command.Save(obj.ToList(),apr.UserId);
                            break;
                        }
                    case "Discharge":
                        {
                            Discharge command = new Discharge();
                            command = JsonConvert.DeserializeObject<Discharge>(apr.ApiPacket.Packet.ToString());
                            commandResult = command.Save(command);
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
                }*/

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
