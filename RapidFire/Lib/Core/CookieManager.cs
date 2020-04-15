using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RapidFireLib.Lib.Core
{
    public class CookieManager
    {
        private HttpContext httpContext;
        public CookieManager()
        {
            httpContext = new Http().HttpContext;
        }
        public void Set(string key, string value, int? expireTime = 1440)
        {
            CookieOptions option = new CookieOptions()
            {
                Path = "/",
                HttpOnly = true,
                Secure = false
            };
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            httpContext.Response.Cookies.Append(key, value, option);
        }
        public void Remove(string key)
        {
            httpContext.Response.Cookies.Delete(key);
        }
        public string Get(string key)
        {
            return httpContext.Request.Cookies[key];
        }
    }
}
