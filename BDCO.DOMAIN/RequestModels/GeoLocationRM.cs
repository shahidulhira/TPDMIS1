namespace BDCO.Domain.Query
{
    public class GeoLocationRM
    {
        public int UserID { get; set; }
        public string BenID { get; set; }
        public string BenNameEn { get; set; }
        public string BenNameBn { get; set; }
        public int DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string DivisionNameBangla { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameBangla { get; set; }
        public string UpazilaCode { get; set; }
        public string UpazilaName { get; set; }
        public string UpazilaNameBangla { get; set; }
        public string UnionCode { get; set; }
        public string UnionName { get; set; }
        public string UnionNameBangla { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
        public string VillageNameBangla { get; set; }
    }


    public class TPDGeoLocationRM
    {
        public string GeoType { get; set; }
    }
    public class PermittedGeoLocationRM
    {
        public int UserId { get; set; }
        public string GeoType { get; set; }
    }

}
