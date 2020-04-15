using BDCO.Web.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Web.Models
{
    public class RegistrationSearchParam
    {
        [Display(Name = "RegistrationSearchHeader", ResourceType = typeof(Resource))]
        public int RegistrationSearchHeader { get; set; }


        [Display(Name = "UserId", ResourceType = typeof(Resource))]
        public int UserId { get; set; }
        [Display(Name = "FromDate", ResourceType = typeof(Resource))]
        public DateTime? FromDate { get; set; }

        [Display(Name = "ToDate", ResourceType = typeof(Resource))]
        public DateTime? ToDate { get; set; }

        [Display(Name = "ServicePointId", ResourceType = typeof(Resource))]
        public string ServicePointId { get; set; }

        [Display(Name = "DistrictCode", ResourceType = typeof(Resource))]
        public string DistrictCode { get; set; }
        [Display(Name = "GeoType", ResourceType = typeof(Resource))]
        public string GeoType { get; set; }
        [Display(Name = "UpazilaCode", ResourceType = typeof(Resource))]
        public string UpazilaCode { get; set; }

        [Display(Name = "UnionCode", ResourceType = typeof(Resource))]
        public string UnionCode { get; set; }

        [Display(Name = "VillageCode", ResourceType = typeof(Resource))]
        public string VillageCode { get; set; }

        [Display(Name = "MobileNo", ResourceType = typeof(Resource))]
        public string MobileNo { get; set; }

        [Display(Name = "vacBenId", ResourceType = typeof(Resource))]
        public string BenId { get; set; }





        [Display(Name = "BenFName", ResourceType = typeof(Resource))]

        public string BenFName { get; set; }


        [Display(Name = "BenMName", ResourceType = typeof(Resource))]
        public string BenMName { get; set; }

        [Display(Name = "BenLName", ResourceType = typeof(Resource))]
        public string BenLName { get; set; }





        [Display(Name = "BenName", ResourceType = typeof(Resource))]
        public string BenName { get; set; }

        [Display(Name = "HomeNo", ResourceType = typeof(Resource))]
        public string HomeNo { get; set; }

        [Display(Name = "HomeName", ResourceType = typeof(Resource))]
        public string HomeName { get; set; }

        [Display(Name = "HouseholdNo", ResourceType = typeof(Resource))]
        public string HouseholdNo { get; set; }

        [Display(Name = "HouseID", ResourceType = typeof(Resource))]
        public string HouseID { get; set; }

        [Display(Name = "HeadOfTheFamily", ResourceType = typeof(Resource))]
        public string HeadOfTheFamily { get; set; }


        [Display(Name = "HeadOccupationCode", ResourceType = typeof(Resource))]
        public string HeadOccupationCode { get; set; }


        [Display(Name = "FatherHusbandName", ResourceType = typeof(Resource))]
        public string FatherHusbandName { get; set; }



        [Display(Name = "FamilyTitle", ResourceType = typeof(Resource))]
        public string FamilyTitle { get; set; }

        [Display(Name = "Language", ResourceType = typeof(Resource))]
        public string Language { get; set; }

        [Display(Name = "NumberOfMember", ResourceType = typeof(Resource))]
        public string NumberOfMember { get; set; }

        [Display(Name = "DataProviderName", ResourceType = typeof(Resource))]
        public string DataProviderName { get; set; }

        [Display(Name = "DataCollectionDate", ResourceType = typeof(Resource))]
        public string DataCollectionDate { get; set; }

        [Display(Name = "Commodities", ResourceType = typeof(Resource))]
        public string Commodities { get; set; }

        [Display(Name = "HouseCondition", ResourceType = typeof(Resource))]
        public string HouseCondition { get; set; }

        [Display(Name = "DomesticAnimals", ResourceType = typeof(Resource))]
        public string DomesticAnimals { get; set; }


        [Display(Name = "MonthlyIncome", ResourceType = typeof(Resource))]
        public string MonthlyIncome { get; set; }

        [Display(Name = "MobileMessage", ResourceType = typeof(Resource))]
        public string MobileMessage { get; set; }


        [Display(Name = "TVMessage", ResourceType = typeof(Resource))]
        public string TVMessage { get; set; }

        [Display(Name = "FiveHabitsPoster", ResourceType = typeof(Resource))]
        public string FiveHabitsPoster { get; set; }

        [Display(Name = "AssistRecycling", ResourceType = typeof(Resource))]
        public string AssistRecycling { get; set; }

        [Display(Name = "WaterBoilMicking", ResourceType = typeof(Resource))]
        public string WaterBoilMicking { get; set; }
        [Display(Name = "VerificationDate", ResourceType = typeof(Resource))]
        public string VerificationDate { get; set; }

        [Display(Name = "VerificationNote", ResourceType = typeof(Resource))]
        public string VerificationNote { get; set; }

        [Display(Name = "IsVerified", ResourceType = typeof(Resource))]
        public string IsVerified { get; set; }

        [Display(Name = "Verification", ResourceType = typeof(Resource))]
        public string Verification { get; set; }

        [Display(Name = "BenInfo", ResourceType = typeof(Resource))]
        public string BenInfo { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Resource))]
        public string Gender { get; set; }


        [Display(Name = "MaritalStatus", ResourceType = typeof(Resource))]
        public string MaritalStatus { get; set; }


        [Display(Name = "IsPregnant", ResourceType = typeof(Resource))]
        public string IsPregnant { get; set; }


        [Display(Name = "DateOfBirth", ResourceType = typeof(Resource))]
        public string DateOfBirth { get; set; }


        [Display(Name = "Age", ResourceType = typeof(Resource))]
        public string Age { get; set; }

        [Display(Name = "Month", ResourceType = typeof(Resource))]
        public string Month { get; set; }

        [Display(Name = "Year", ResourceType = typeof(Resource))]
        public string Year { get; set; }

        [Display(Name = "Day", ResourceType = typeof(Resource))]
        public string Day { get; set; }


        [Display(Name = "IsGuessed", ResourceType = typeof(Resource))]
        public string IsGuessed { get; set; }



        [Display(Name = "HasBirthCertificate", ResourceType = typeof(Resource))]
        public string HasBirthCertificate { get; set; }


        [Display(Name = "HasAutism", ResourceType = typeof(Resource))]
        public string HasAutism { get; set; }


        [Display(Name = "EduQualification", ResourceType = typeof(Resource))]
        public string EduQualification { get; set; }


        [Display(Name = "RelationWithHead", ResourceType = typeof(Resource))]
        public string RelationWithHead { get; set; }


        [Display(Name = "ServicePointing", ResourceType = typeof(Resource))]
        public string ServicePointing { get; set; }


        [Display(Name = "BenType", ResourceType = typeof(Resource))]
        public string BenType { get; set; }


        [Display(Name = "RegistrationDate", ResourceType = typeof(Resource))]
        public string RegistrationDate { get; set; }


        [Display(Name = "OccupationCode", ResourceType = typeof(Resource))]
        public string OccupationCode { get; set; }



        [Display(Name = "ServicePointType", ResourceType = typeof(Resource))]
        public string ServicePointType { get; set; }



        [Display(Name = "ServicePointName", ResourceType = typeof(Resource))]
        public string ServicePointName { get; set; }



        [Display(Name = "IsChildLabor", ResourceType = typeof(Resource))]
        public string IsChildLabor { get; set; }



        [Display(Name = "IcdpService", ResourceType = typeof(Resource))]
        public string IcdpService { get; set; }



        [Display(Name = "RcvSvcCode", ResourceType = typeof(Resource))]
        public string RcvSvcCode { get; set; }



        [Display(Name = "SvcRcvFromOrg", ResourceType = typeof(Resource))]
        public string SvcRcvFromOrg { get; set; }



        [Display(Name = "OthSvcProviderOrgName", ResourceType = typeof(Resource))]
        public string OthSvcProviderOrgName { get; set; }



        [Display(Name = "IsActiveMobileNo", ResourceType = typeof(Resource))]
        public string IsActiveMobileNo { get; set; }



        [Display(Name = "WantMobileMssg", ResourceType = typeof(Resource))]
        public string WantMobileMssg { get; set; }




        [Display(Name = "Enrolled", ResourceType = typeof(Resource))]
        public string Enrolled { get; set; }



        [Display(Name = "OtherNGO", ResourceType = typeof(Resource))]
        public string OtherNGO { get; set; }



        [Display(Name = "ProjectCode", ResourceType = typeof(Resource))]
        public string ProjectCode { get; set; }



        [Display(Name = "NationalId", ResourceType = typeof(Resource))]
        public string NationalId { get; set; }




        [Display(Name = "BirthId", ResourceType = typeof(Resource))]
        public string BirthId { get; set; }



        [Display(Name = "DrivingId", ResourceType = typeof(Resource))]
        public string DrivingId { get; set; }



        [Display(Name = "PassportId", ResourceType = typeof(Resource))]
        public string PassportId { get; set; }


        [Display(Name = "newRemarks", ResourceType = typeof(Resource))]
        public string newRemarks { get; set; }


        [Display(Name = "SponsorId", ResourceType = typeof(Resource))]
        public string SponsorId { get; set; }



        [Display(Name = "ServicePointAttendance", ResourceType = typeof(Resource))]
        public string ServicePointAttendance { get; set; }



        [Display(Name = "Personality", ResourceType = typeof(Resource))]
        public string Personality { get; set; }



        [Display(Name = "HHServices", ResourceType = typeof(Resource))]
        public string HHServices { get; set; }



        [Display(Name = "Location", ResourceType = typeof(Resource))]
        public string Location { get; set; }



        [Display(Name = "HHInfo", ResourceType = typeof(Resource))]
        public string HHInfo { get; set; }

        [Display(Name = "FamilyMember", ResourceType = typeof(Resource))]
        public string FamilyMember { get; set; }



        [Display(Name = "SponsorChildInfo", ResourceType = typeof(Resource))]
        public string SponsorChildInfo { get; set; }




        [Display(Name = "ChildId", ResourceType = typeof(Resource))]
        public string ChildId { get; set; }

        [Display(Name = "DailyActivities", ResourceType = typeof(Resource))]
        public string DailyActivities { get; set; }

        [Display(Name = "FavoriteSubject", ResourceType = typeof(Resource))]
        public string FavoriteSubject { get; set; }

        [Display(Name = "GradeInServicePoint", ResourceType = typeof(Resource))]
        public string GradeInServicePoint { get; set; }


        [Display(Name = "Ambition", ResourceType = typeof(Resource))]
        public string Ambition { get; set; }


        [Display(Name = "FavoritePlayItem", ResourceType = typeof(Resource))]
        public string FavoritePlayItem { get; set; }


        [Display(Name = "Walking", ResourceType = typeof(Resource))]
        public string Walking { get; set; }


        [Display(Name = "Talking", ResourceType = typeof(Resource))]
        public string Talking { get; set; }


        [Display(Name = "Crawling", ResourceType = typeof(Resource))]
        public string Crawling { get; set; }


        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }


        [Display(Name = "IneligibilityEvent", ResourceType = typeof(Resource))]
        public string IneligibilityEvent { get; set; }


        [Display(Name = "IneligibilityEventDate", ResourceType = typeof(Resource))]
        public string IneligibilityEventDate { get; set; }

        //[Display(Name = "VerificationNote", ResourceType = typeof(Resource))]
        //public string VerificationNote { get; set; }

        //[Display(Name = "GradeInServicePoint", ResourceType = typeof(Resource))]
        //public string GradeInServicePoint { get; set; }

        //[Display(Name = "GradeInServicePoint", ResourceType = typeof(Resource))]
        //public string GradeInServicePoint { get; set; }

        //[Display(Name = "GradeInServicePoint", ResourceType = typeof(Resource))]
        //public string GradeInServicePoint { get; set; }

        //[Display(Name = "GradeInServicePoint", ResourceType = typeof(Resource))]
        //public string GradeInServicePoint { get; set; }

        //[Display(Name = "GradeInServicePoint", ResourceType = typeof(Resource))]
        //public string GradeInServicePoint { get; set; }

        //[Display(Name = "GradeInServicePoint", ResourceType = typeof(Resource))]
        //public string GradeInServicePoint { get; set; }

        //[Display(Name = "GradeInServicePoint", ResourceType = typeof(Resource))]
        //public string GradeInServicePoint { get; set; }

        //[Display(Name = "GradeInServicePoint", ResourceType = typeof(Resource))]
        //public string GradeInServicePoint { get; set; }

        //[Display(Name = "GradeInServicePoint", ResourceType = typeof(Resource))]
        //public string GradeInServicePoint { get; set; }

        //[Display(Name = "GradeInServicePoint", ResourceType = typeof(Resource))]
        //public string GradeInServicePoint { get; set; }
    }
}
