using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RapidFireLib.Extensions
{
    public static class CookieExtension
    {
        public static string GetValue(this IRequestCookieCollection requestCookieCollection,string key)
        {
            return requestCookieCollection.GetValue(key);
        }
    }
}
