using System;

namespace RapidFireLib.Models.Api
{
    public class ApiPacketRequest
    {
        public int UserId { get; set; }
        public String TableName { get; set; }
        public String Command { get; set; }
        public int TotalRecord { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public GeoPacket GeoPacket { get; set; }
        public ApiPacket<Object> ApiPacket = new ApiPacket<object>();
    }
}