using BDCO.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates
{
    [Table("UserGeoLocation")]
    public class UserGeoLocation : AggregateRoot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string DivisionCode { get; set; }
        //public string DivisionName { get; set; }
        public string DistrictCode { get; set; }
        // public string DistrictName { get; set; }
        public string UpazilaCode { get; set; }
        // public string UpazilaName { get; set; }
        public string UnionCode { get; set; }
        // public string UnionName { get; set; }
        public string VillageCode { get; set; }
        // public string VillageName { get; set; }
        public string CenterId { get; set; }
        public int? CampId { get; set; }
        public int? BlockId { get; set; }


        private readonly IRepository<UserGeoLocation> repositoryUserGeoLocation = new Repository<UserGeoLocation>();


        //public AspNetUsersLoginResult Login(AspNetUsersLogin )
        //{
        //    repositoryANCInfoView.SetContext(context.Default);
        //    ANCViewResult result = new ANCViewResult();
        //    string sql = @"SELECT MasterCode, DivisionName AS Division, DistrictName AS District, UpazilaName AS Upazila,FacilityName,
        //    strftime('%d-%m-%Y', ObservationDate) ObservationDate, 
        //    CASE AssessmentType WHEN 1 THEN 'Baseline' WHEN 2 THEN 'Regular' WHEN 3 THEN 'Endline' END AssessmentType
        //    FROM ANCInfo anc
        //    INNER JOIN Division div ON div.DivisionCode=anc.DivisionCode
        //    INNER JOIN District dis ON dis.DistrictCode=anc.DistrictCode AND dis.DivisionCode=anc.DivisionCode
        //    INNER JOIN Upazila upz ON  upz.UpazilaCode=anc.UpazilaCode AND upz.DistrictCode=anc.DistrictCode e";
        //    result.lstANCInfoView = repositoryANCInfoView.GetRecordSetList(sql);
        //    return result;
        //}

    }

    public class UserGeoLocationModel
    {
        public string DivisionCode { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }

    }

    public class UserBlockModel
    {
        public int? BlockId { get; set; }
        public string BlockName { get; set; }
        public string BlockFullName { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public int? CenterId { get; set; }
        public int? CampId { get; set; }
    }

    [Table("UserServicePoint")]
    public class UserServicePoint : AggregateRoot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string ServicePointId { get; set; }
        public DateTime? Date { get; set; }

        private readonly IRepository<UserServicePoint> repository = new Repository<UserServicePoint>();


    }
}
