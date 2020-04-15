using BDCO.Domain.Aggregates;
using BDCO.Domain.Identity;
using BDCO.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDCO.Web.Models
{
    public class LoginResponse
    {
        public LoginUserInfo UserInfo = new LoginUserInfo();        
       // public List<UserBlockModel> BlockInfo = new List<UserBlockModel>();
       
    }
}