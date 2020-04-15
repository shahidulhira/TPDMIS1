using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Aggregates
{
    public class CommunityLearningFacilityChecklist
    {
        [Key]
        public long ChecklistId;
        public long FacilityId;
        public string CampNo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
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
        public string Q1_5 { get; set; }
        public string Q1_5_Rem { get; set; }
        public string Q1_6 { get; set; }
        public string Q1_6_Rem { get; set; }
        public string Q1_7 { get; set; }
        public string Q1_7_Rem { get; set; }
        public string Q1_8 { get; set; }
        public string Q1_8_Rem { get; set; }
        public string Q1_9 { get; set; }
        public string Q1_9_Rem { get; set; }
        public string Q1_10 { get; set; }
        public string Q1_10_Rem { get; set; }
        public string Q1_11 { get; set; }
        public string Q1_11_Rem { get; set; }
        public string Q1_12 { get; set; }
        public string Q1_12_Rem { get; set; }
        public string Q2_1 { get; set; }
        public string Q2_1_Rem { get; set; }
        public string Q2_2 { get; set; }
        public string Q2_2_Rem { get; set; }
        public string Q2_3 { get; set; }
        public string Q2_3_Rem { get; set; }
        public string Q3_1 { get; set; }
        public string Q3_1_Rem { get; set; }
        public string Q3_2 { get; set; }
        public string Q3_2_Rem { get; set; }
        public string Q3_3 { get; set; }
        public string Q3_3_Rem { get; set; }
        public string Q3_4 { get; set; }
        public string Q3_4_Rem { get; set; }
        public string Q3_5 { get; set; }
        public string Q3_5_Rem { get; set; }
        public string Q3_6 { get; set; }
        public string Q3_6_Rem { get; set; }
        public string Q3_7 { get; set; }
        public string Q3_7_Rem { get; set; }
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
