using BDCO.Domain.Aggregates;
using BDCO.Domain.Identity;
using BDCO.Domain.Models;
using System.Collections.Generic;

namespace BDCO.Domain.Result
{
    public class LoginResponseResult
    {
        public LoginUserInfo UserInfo = new LoginUserInfo();        
        //public List<UserBlockModel> BlockInfo = new List<UserBlockModel>();
       
    }
}
