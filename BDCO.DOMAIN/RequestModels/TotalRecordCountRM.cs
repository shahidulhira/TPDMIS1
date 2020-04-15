using BDCO.Domain.Aggregates;

namespace BDCO.Domain.RequestModels
{
    public class TotalRecordCountRM
    {
        public string TableName { get; set; }
        public int UserId { get; set; }
        public GeoPacket GeoPacket { get; set; }
    }
}
