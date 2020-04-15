using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Aggregates
{
    public class ProfileInfo
    {
        [Key]
        public string ProfileId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string CampId { get; set; }
        public string BlockId { get; set; }
        public string Mobile { get; set; }
        public string MOHAID { get; set; }
        public string NID { get; set; }
        public string Address { get; set; }
        public string Designation { get; set; }
        public string PartnerId { get; set; }
        public string EducationQualification { get; set; }
        public string ExperienceInField { get; set; }
        public string FacilityId { get; set; }
        public string TeachingSubject { get; set; }
        public string TeacherLearningCycleByRT { get; set; }
        public string TrainingReceived { get; set; }
        public string TrainingOrganizedBy { get; set; }
        public string TrainingExperienceOutOfCamp { get; set; }
        public string TrainingExperienceInCamp { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorMobile { get; set; }
        public string SupervisorEmail { get; set; }
        public string DeviceId { get; set; }
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
