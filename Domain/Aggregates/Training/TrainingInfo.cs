using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Domain.Aggregates
{
    [Table("TrainingInfo")]
    public class TrainingInfo
    {
        public TrainingInfo()
        {
            TrainingInfoTopic = new List<TrainingInfoTopic>();
            TrainingInfoMaterial = new List<TrainingInfoMaterial>();
        }
        public long RecordID { get; set; }
        [Key]
        public string TrainingId { get; set; }
        public string TrainingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TypeId { get; set; }
        public string CategoryId { get; set; }
        public string TopicOthers { get; set; }
        public string Venue { get; set; }
        public string Trainer { get; set; }
        public string BatchCode { get; set; }
        public string Remarks { get; set; }


        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string CenterCode { get; set; }
        public DateTime? DataCollectionDate { get; set; }
        public string DataCollectionTime { get; set; }
        public int? UserId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string VerificationNote { get; set; }


        public List<TrainingInfoMaterial> TrainingInfoMaterial { get; set; }
        public List<TrainingInfoTopic> TrainingInfoTopic { get; set; }

        //public List<TrainingMemberInfo> TrainingMemberInfo { get; set; }
        //public List<TrainingInfoTopic> TrainingInfoTopic { get; set; }
        //public List<TrainingInfoMaterial> TrainingInfoMaterial { get; set; }
    }
}