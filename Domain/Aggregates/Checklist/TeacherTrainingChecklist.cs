using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates
{
    [Table("TeacherTrainingChecklist")]
    public class TeacherTrainingChecklist
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordID { get; set; }
        [Key]
        public string ChecklistId { get; set; }
        public string TypeId { get; set; }
        public string CategoryId { get; set; }
        public string BatchCode { get; set; }
        public string NoOfParticipant { get; set; }
        public string TrainingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Venue { get; set; }
        public string Trainer { get; set; }
        public string VisitorName { get; set; }
        public string VisitorDesignation { get; set; }
        public string VisitDate { get; set; }
        public string Q1_1 { get; set; }
        public string Q1_1_Rem { get; set; }
        public string Q1_2 { get; set; }
        public string Q1_2_Rem { get; set; }
        public string Q1_3 { get; set; }
        public string Q1_3_Rem { get; set; }
        public string Q1_4 { get; set; }
        public string Q1_4_Rem { get; set; }
        public string Q2_1 { get; set; }
        public string Q2_1_Rem { get; set; }
        public string Q2_2 { get; set; }
        public string Q2_2_Rem { get; set; }
        public string Q2_3 { get; set; }
        public string Q2_3_Rem { get; set; }
        public string Q2_4 { get; set; }
        public string Q2_4_Rem { get; set; }
        public string Q3_1 { get; set; }
        public string Q3_1_Rem { get; set; }
        public string Q3_2 { get; set; }
        public string Q3_2_Rem { get; set; }
        public string Q3_3 { get; set; }
        public string Q3_3_Rem { get; set; }
        public string Q3_4 { get; set; }
        public string Q3_4_Rem { get; set; }
        public string Q4_1 { get; set; }
        public string Q4_1_Rem { get; set; }
        public string Q4_2 { get; set; }
        public string Q4_2_Rem { get; set; }
        public string Q4_3 { get; set; }
        public string Q4_3_Rem { get; set; }
        public string Q5_All_Comments { get; set; }
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