﻿namespace BDCO.Domain.Aggregates
{
    public class GeoPacket
    {
        public string ServicePointId { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public int? CenterId { get; set; }

        public GeoPacket(string servicePointId, string districtCode, string upazilaCode, string unionCode, string villageCode,int centerId)
        {
            ServicePointId = servicePointId;
            DistrictCode = districtCode;
            UpazilaCode = upazilaCode;
            UnionCode = unionCode;
            VillageCode = villageCode;
            CenterId = centerId;
        }
    }
}
