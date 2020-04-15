using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Aggregates
{
    public class UserGeolocation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGeoId { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public int? GeoType { get; set; }
        public int UserId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    //Outgoing
    public class PermittedGeoLocation
    {
        public List<District> District { get; set; }
        public List<Upazila> Upazila { get; set; }
        public List<Union> Unions { get; set; }
        public List<Village> Village { get; set; }
    }

    #region Geoinfo

    [Table("viewDistrict")]
    public class District
    {
        public string DivisionCode { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameBangla { get; set; }
    }
    [Table("viewUpazila")]
    public class Upazila
    {
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UpazilaName { get; set; }
        public string UpazilaNameBangla { get; set; }
    }
    [Table("viewUnion")]
    public class Union
    {
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string UnionName { get; set; }
        public string UnionNameBangla { get; set; }
    }
    [Table("viewVillage")]
    public class Village
    {
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
        public string VillageNameBangla { get; set; }
    }

    #endregion


    [Table("UserGeolocationView")]
    public class UserGeo
    {
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
        public int UserId { get; set; }
    }
}
