using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Aggregates
{
    public class CompetencyTest
    {        
        [Key]
        public string CompetencyTestId { get; set; }
        public string ProfileId { get; set; }
        public string ExamTestId { get; set; }
        public string ExaminerId { get; set; }
        public string FacilityId { get; set; }
        public string TeachingLevel { get; set; }
        public string PartnerId { get; set; }
        public string LOneEnglish { get; set; }
        public string LTwoEnglish { get; set; }
        public string LThreeEnglish { get; set; }
        public string LFourEnglish { get; set; }
        public string LOneBurmese { get; set; }
        public string LTwoBurmese { get; set; }
        public string LThreeBurmese { get; set; }
        public string LFourBurmese { get; set; }
        public string LOneScience { get; set; }
        public string LTwoScience { get; set; }
        public string LThreeScience { get; set; }
        public string LFourScience { get; set; }
        public string LOneMath { get; set; }
        public string LTwoMath { get; set; }
        public string LThreeMath { get; set; }
        public string LFourMath { get; set; }
        public string LOneLifeSkills { get; set; }
        public string LTwoLifeSkills { get; set; }
        public string LThreeLifeSkills { get; set; }
        public string LFourLifeSkills { get; set; }

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
