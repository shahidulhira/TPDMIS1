using BDCO.Domain.Aggregates;

namespace BDCO.Domain.RequestParams
{ 
    public class ForApiResponse
    {
        public int UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public GeoPacket GeoPacket { get; set; }

    }
}
