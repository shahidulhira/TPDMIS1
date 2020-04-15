namespace RapidFireLib.Models.Api
{
    public class GeoPacket
    {
        public string ServicePointId { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }

        public GeoPacket(string servicePointId, string districtCode, string upazilaCode, string unionCode, string villageCode)
        {
            ServicePointId = servicePointId;
            DistrictCode = districtCode;
            UpazilaCode = upazilaCode;
            UnionCode = unionCode;
            VillageCode = villageCode;
        }
    }
}
