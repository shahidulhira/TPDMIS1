using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Aggregates
{
    public class LearningFacility
    {
        [Key]
        public long FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityTypeId { get; set; }
        public string FacilityStatus { get; set; }
        public long? PartnerId { get; set; }
        public int? CampId { get; set; }
        public int? BlockId { get; set; }
        public string DurationOfShift { get; set; }
        public int? SpecialBoys { get; set; }
        public int? SpecialGirls { get; set; }
        public int? Level1Boys { get; set; }
        public int? Level1Girls { get; set; }
        public int? Level2Boys { get; set; }
        public int? Level2Girls { get; set; }
        public int? Level3Boys { get; set; }
        public int? Level3Girls { get; set; }
        public int? Level4Boys { get; set; }
        public int? Level4Girls { get; set; }
        public int? Level5Boys { get; set; }
        public int? Level5Girls { get; set; }
        public int? HasLCMCnCEC { get; set; }
        public int? LCMCnCECMale { get; set; }
        public int? LCMCnCECFemale { get; set; }
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
