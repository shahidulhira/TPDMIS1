using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Aggregates
{
    public class DocAndRecordChecklist
    {        
        [Key]
        public string ChecklistId { get; set; }
        public string ProfileId { get; set; }
        public string Name { get; set; }
        public string ObserverName { get; set; }
        public string ObservationDate { get; set; }
        public string Q2_1_1 { get; set; }
        public string Q2_4_1 { get; set; }
        public string Q2_4_2 { get; set; }
        public string Q4_2_3 { get; set; }
        public string Q4_4_2 { get; set; }

        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string CenterCode { get; set; }
        public string DataCollectionDate { get; set; }
        public string DataCollectionTime { get; set; }
        public int? UserId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? Status { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModificationDate { get; set; }
        public int? IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public string VerificationDate { get; set; }
        public string VerificationNote { get; set; }
    }
}
