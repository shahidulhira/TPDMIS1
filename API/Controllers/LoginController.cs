using Api.Config;
using Domain.Aggregates.Identity;
using Microsoft.AspNetCore.Mvc;
using RapidFireLib.Lib.Authintication;
using RapidFireLib.Lib.Core;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        RapidFire rf = new RapidFire(new AppConfig(), false);

        [Route("/api/login")]
        [HttpPost]
        public IActionResult Index(LoginUser loginUser)
        {
            var loginResult = rf.UserAuthintications.Authenticate(new AspNetUser(), loginUser.Username, loginUser.Password, true, LoginType.DbLogin);
            if (loginResult == null)
                return BadRequest("invalid credential");
            loginResult = GetLoginResult(loginResult, loginUser);
            return Ok(loginResult);
        }
        private User GetLoginResult(User loginResult, LoginUser loginUser)
        {
            var deviceInfos = rf.Db.Get<DeviceInfo>(x => x.UserId == loginResult.UserId && x.DeviceUniqueId == loginUser.DeviceUniqueId).FirstOrDefault();
            if (deviceInfos == null)
            {
                DeviceInfo deviceInfo = new DeviceInfo()
                {
                    DeviceUniqueId = loginUser.DeviceUniqueId,
                    UserId = loginResult.UserId
                };
                var obj = rf.Db.Save(deviceInfo);
                rf.Db.Commit();
                deviceInfo.DeviceId = ((DeviceInfo)(obj.Result.Model)).DeviceInfoId.ToString().PadLeft(5, '0');
                rf.Db.Save(deviceInfo);
                rf.Db.Commit();
                loginResult.DeviceId = deviceInfo.DeviceId;
            }
            else
            {
                if (string.IsNullOrEmpty(deviceInfos.DeviceId))
                {
                    deviceInfos.DeviceId = (deviceInfos).DeviceInfoId.ToString().PadLeft(5, '0');
                    rf.Db.Save(deviceInfos);
                    rf.Db.Commit();
                }
                loginResult.DeviceId = deviceInfos.DeviceId;
            }
            var userInfo = rf.Db.Get<AspNetUser>(x => x.UserId == loginResult.UserId).FirstOrDefault();
            //loginResult.GeoType = userInfo.GeoType;
            loginResult.FullName = userInfo.FullName;
            loginResult.Designation = userInfo.Designation;
            loginResult.Organization = userInfo.Organization;
            loginResult.StaffID = userInfo.StaffID;
            loginResult.PhoneNumber = userInfo.PhoneNumber;
            loginResult.CreateDate = userInfo.CreateDate.ToString();
            loginResult.EditDate = userInfo.EditDate.ToString();
            loginResult.Password = userInfo.Password;
            return loginResult;
        }
    }


}