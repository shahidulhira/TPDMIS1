using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDCO.Web.Models.Api
{
    public class ApiPacket<T>
    {
        public T Packet { get; set; }
        public List<T> PacketList { get; set; }
    }
}