using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RapidFireLib.Models.Api
{
    public class FetchPacketRequest
    {
        public int UserId { get; set; }
        public String TableName { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}