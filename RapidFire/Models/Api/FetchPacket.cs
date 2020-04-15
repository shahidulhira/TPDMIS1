using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RapidFireLib.Models.Api
{
    public class FetchPacket<T>
    {
        public List<T> Packet { get; set; }
        public int PageNo { get; set; }
        public int TotalCount { get; set; }
    }
}