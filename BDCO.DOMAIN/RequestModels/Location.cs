namespace BDCO.Domain.RequestModels
{
    public class DivisionRM
    {
        public string UserID { get; set; }

    }
    public class DistrictRM
    {
        public string DivisionCode { get; set; }
        public string UserID { get; set; }

    }
    public class DistrictByUserIdRM
    {
        public string DivisionCode { get; set; }
        public string UserID { get; set; }

    }

    public class DistrictByDivisionRM
    {
        public string DivisionCode { get; set; }
        public string UserID { get; set; }

    }

    public class DistrictByGeolocationRM
    {
        //public string DivisionCode { get; set; }
        public string UserID { get; set; }

    }

    public class UpazilaRM
    {
        public string DistrictCode { get; set; }
        public string UserID { get; set; }
        public string UpazilaName { get; set; }
        public string GeoType { get; set; }

    }

    public class UpazilaByUserIdRM
    {
        public string DistrictCode { get; set; }
        public string UserID { get; set; }
        public string NGOID { get; set; }

    }

    public class UnionRM
    {
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UserID { get; set; }
        public string UnionName { get; set; }

    }

    public class VillageRM
    {
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string UserID { get; set; }
        public string VillageName { get; set; }
    }
}
