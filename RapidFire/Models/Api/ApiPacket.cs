using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RapidFireLib.Models.Api
{
    public class ApiPacket<T>
    {
        public T Packet { get; set; }
        public List<T> PacketList { get; set; }
    }
}