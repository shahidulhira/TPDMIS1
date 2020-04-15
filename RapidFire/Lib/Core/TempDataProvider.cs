using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;

namespace RapidFireLib.Lib.Core
{
    public class TempDataProvider : ITempDataProvider
    {
        public const string TempDataCookieKey = "__ControllerTempData";
        public IDictionary<string, object> LoadTempData(HttpContext context)
        {
            Dictionary<string, object> tempDataDictionary = new Dictionary<string, object>();
            return tempDataDictionary;
        }
        public void SaveTempData(ControllerContext controller, IDictionary<string, object> values)
        {
            bool hasValues = (values != null && values.Count > 0);
        }
        public void SaveTempData(HttpContext context, IDictionary<string, object> values)
        {
            throw new NotImplementedException();
        }
    }
}
