using Api.Config;
using Domain.Handlers.Api.Common;
using Microsoft.AspNetCore.Mvc;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models.Api;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    public class FetchAPIController : ControllerBase
    {
        RapidFire rf = new RapidFire(new AppConfig());

        [HttpPost]
        [Route("api/Fetch/GetPacket")]
        // [Authorize]
        public async Task<ApiResponse<object>> GetPacketByUserId(ApiPacketRequest apr)
        {

            ApiResponse<object> apiResponse = new ApiResponse<object>();
            try
            {
                switch (apr.TableName)
                {
                    case "UserGeo":
                        apiResponse = await rf.Api.ProcessFetchAsync(apr, null, new FetchGeolocationHandler());
                        break;
                    default:
                        apiResponse = await rf.Api.ProcessFetchAsync(apr, null, null);
                        break;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            //CallerId and Command Bindings ---------- Start
            //apiResponse.CallerId = apr.CallerId;
            apiResponse.Command = apr.Command;
            //CallerId and Command Bindings ---------- End

            return apiResponse;
        }
    }
}
