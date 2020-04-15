using BDCO.Web.App_LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BDCO.Web.Models
{
    public class SearchParam
    {
        [Display(Name = "AncRegisterTitle", ResourceType = typeof(Resource))]
        public string AncRegisterTitle { get; set; }

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






        [Display(Name = "RowNo", ResourceType = typeof(Resource))]
        public string RowNo { get; set; }
        [Display(Name = "vacBenId", ResourceType = typeof(Resource))]
        public string BenId { get; set; }

        [Display(Name = "ancRegistrationDate", ResourceType = typeof(Resource))]
        public string RegistrationDate { get; set; }

        [Display(Name = "BenName", ResourceType = typeof(Resource))]
        public string BenName { get; set; }

        [Display(Name = "DateOfBirth", ResourceType = typeof(Resource))]
        public string DateOfBirth { get; set; }

        [Display(Name = "Age", ResourceType = typeof(Resource))]
        public string Age { get; set; }

        [Display(Name = "FatherOrHusband", ResourceType = typeof(Resource))]
        public string FatherOrHusband { get; set; }


        [Display(Name = "Basic", ResourceType = typeof(Resource))]
        public string Basic { get; set; }

        [Display(Name = "ANC", ResourceType = typeof(Resource))]
        public string ANC { get; set; }

        [Display(Name = "DELIVERY", ResourceType = typeof(Resource))]
        public string DELIVERY { get; set; }

        [Display(Name = "PNC", ResourceType = typeof(Resource))]
        public string PNC { get; set; }


        [Display(Name = "RegistrationInfo", ResourceType = typeof(Resource))]
        public string RegistrationInfo { get; set; }

        [Display(Name = "CommunicationInfo", ResourceType = typeof(Resource))]
        public string CommunicationInfo { get; set; }

        [Display(Name = "BasicBenId", ResourceType = typeof(Resource))]
        public string BasicBenId { get; set; }

        [Display(Name = "BasicDataCollectionDate", ResourceType = typeof(Resource))]
        public string BasicDataCollectionDate { get; set; }


        [Display(Name = "MobileNo", ResourceType = typeof(Resource))]
        public string MobileNo { get; set; }
        [Display(Name = "AncInfo", ResourceType = typeof(Resource))]
        public string AncInfo { get; set; }


        [Display(Name = "DeliveryInfo", ResourceType = typeof(Resource))]
        public string DeliveryInfo { get; set; }


        [Display(Name = "PncInfo", ResourceType = typeof(Resource))]
        public string PncInfo { get; set; }

        [Display(Name = "tt_vaccineInfo", ResourceType = typeof(Resource))]
        public string tt_vaccineInfo { get; set; }



        [Display(Name = "ancLmp", ResourceType = typeof(Resource))]
        public string ancLmp { get; set; }

        [Display(Name = "ancEdd", ResourceType = typeof(Resource))]
        public string ancEdd { get; set; }

        [Display(Name = "ancTTDate", ResourceType = typeof(Resource))]
        public string ancTTDate { get; set; }

        [Display(Name = "ancTotalDose", ResourceType = typeof(Resource))]
        public string ancTotalDose { get; set; }


        [Display(Name = "ancVisitNo", ResourceType = typeof(Resource))]
        public string ancVisitNo { get; set; }


        [Display(Name = "ancAncVisitDate", ResourceType = typeof(Resource))]
        public string ancAncVisitDate { get; set; }

        [Display(Name = "ancRemarks", ResourceType = typeof(Resource))]
        public string ancRemarks { get; set; }

        [Display(Name = "pncDeliveryDate", ResourceType = typeof(Resource))]
        public string pncDeliveryDate { get; set; }


        [Display(Name = "pncDeliveryPlace", ResourceType = typeof(Resource))]
        public string pncDeliveryPlace { get; set; }

        [Display(Name = "pncDeliveryAttendant", ResourceType = typeof(Resource))]
        public string pncDeliveryAttendant { get; set; }


        [Display(Name = "pncVisitNo", ResourceType = typeof(Resource))]
        public string pncVisitNo { get; set; }

        [Display(Name = "pncVisitDate", ResourceType = typeof(Resource))]
        public string pncVisitDate { get; set; }

        [Display(Name = "pncRemarks", ResourceType = typeof(Resource))]
        public string pncRemarks { get; set; }



        [Display(Name = "new_info_observation", ResourceType = typeof(Resource))]
        public string new_info_observation { get; set; }


        [Display(Name = "danger_sign", ResourceType = typeof(Resource))]
        public string danger_sign { get; set; }
        [Display(Name = "counseling", ResourceType = typeof(Resource))]
        public string counseling { get; set; }
        [Display(Name = "training_support", ResourceType = typeof(Resource))]
        public string training_support { get; set; }



        [Display(Name = "ancSwelling", ResourceType = typeof(Resource))]
        public string ancSwelling { get; set; }
        [Display(Name = "ancBabyMovingFeel", ResourceType = typeof(Resource))]
        public string ancBabyMovingFeel { get; set; }

        [Display(Name = "ancPaleEyelids", ResourceType = typeof(Resource))]
        public string ancPaleEyelids { get; set; }

        [Display(Name = "ancJaundice", ResourceType = typeof(Resource))]
        public string ancJaundice { get; set; }




        [Display(Name = "yes", ResourceType = typeof(Resource))]
        public string yes { get; set; }
        [Display(Name = "no", ResourceType = typeof(Resource))]
        public string no { get; set; }

        [Display(Name = "not_identified", ResourceType = typeof(Resource))]
        public string not_identified { get; set; }


        [Display(Name = "anc_danger_sign_1", ResourceType = typeof(Resource))]
        public string anc_danger_sign_1 { get; set; }


        [Display(Name = "anc_danger_sign_2", ResourceType = typeof(Resource))]
        public string anc_danger_sign_2 { get; set; }


        [Display(Name = "anc_danger_sign_3", ResourceType = typeof(Resource))]
        public string anc_danger_sign_3 { get; set; }


        [Display(Name = "anc_danger_sign_4", ResourceType = typeof(Resource))]
        public string anc_danger_sign_4 { get; set; }


        [Display(Name = "anc_danger_sign_5", ResourceType = typeof(Resource))]
        public string anc_danger_sign_5 { get; set; }


        [Display(Name = "anc_Counseling_1", ResourceType = typeof(Resource))]
        public string anc_Counseling_1 { get; set; }




        [Display(Name = "anc_Counseling_2", ResourceType = typeof(Resource))]
        public string anc_Counseling_2 { get; set; }

        [Display(Name = "anc_Counseling_3", ResourceType = typeof(Resource))]
        public string anc_Counseling_3 { get; set; }

        [Display(Name = "anc_Counseling_4", ResourceType = typeof(Resource))]
        public string anc_Counseling_4 { get; set; }

        [Display(Name = "anc_Counseling_5", ResourceType = typeof(Resource))]
        public string anc_Counseling_5 { get; set; }

        [Display(Name = "anc_Counseling_6", ResourceType = typeof(Resource))]
        public string anc_Counseling_6 { get; set; }

        [Display(Name = "anc_Counseling_7", ResourceType = typeof(Resource))]
        public string anc_Counseling_7 { get; set; }

        [Display(Name = "anc_Counseling_8", ResourceType = typeof(Resource))]
        public string anc_Counseling_8 { get; set; }

        [Display(Name = "anc_Counseling_9", ResourceType = typeof(Resource))]
        public string anc_Counseling_9 { get; set; }

        [Display(Name = "anc_Counseling_10", ResourceType = typeof(Resource))]
        public string anc_Counseling_10 { get; set; }

        [Display(Name = "anc_Counseling_11", ResourceType = typeof(Resource))]
        public string anc_Counseling_11 { get; set; }

        [Display(Name = "anc_Counseling_12", ResourceType = typeof(Resource))]
        public string anc_Counseling_12 { get; set; }

        [Display(Name = "anc_Counseling_13", ResourceType = typeof(Resource))]
        public string anc_Counseling_13 { get; set; }

        [Display(Name = "anc_Counseling_14", ResourceType = typeof(Resource))]
        public string anc_Counseling_14 { get; set; }

        [Display(Name = "anc_support_1", ResourceType = typeof(Resource))]
        public string anc_support_1 { get; set; }

        [Display(Name = "anc_support_2", ResourceType = typeof(Resource))]
        public string anc_support_2 { get; set; }

        [Display(Name = "anc_support_3", ResourceType = typeof(Resource))]
        public string anc_support_3 { get; set; }

        [Display(Name = "anc_support_4", ResourceType = typeof(Resource))]
        public string anc_support_4 { get; set; }










        [Display(Name = "pnc_info_observation", ResourceType = typeof(Resource))]
        public string pnc_info_observation { get; set; }

        [Display(Name = "pnc_list_item_button1", ResourceType = typeof(Resource))]
        public string pnc_list_item_button1 { get; set; }

        [Display(Name = "pnc_list_item_button2", ResourceType = typeof(Resource))]
        public string pnc_list_item_button2 { get; set; }

        [Display(Name = "pnc_list_item_button3", ResourceType = typeof(Resource))]
        public string pnc_list_item_button3 { get; set; }

        [Display(Name = "pnc_list_item_button4", ResourceType = typeof(Resource))]
        public string pnc_list_item_button4 { get; set; }

        [Display(Name = "pnc_list_item_button5", ResourceType = typeof(Resource))]
        public string pnc_list_item_button5 { get; set; }

        [Display(Name = "pncSwelling", ResourceType = typeof(Resource))]
        public string pncSwelling { get; set; }

        [Display(Name = "pncPaleEyelids", ResourceType = typeof(Resource))]
        public string pncPaleEyelids { get; set; }



        [Display(Name = "pnc_danger_before1", ResourceType = typeof(Resource))]
        public string pnc_danger_before1 { get; set; }

        [Display(Name = "pnc_danger_before2", ResourceType = typeof(Resource))]
        public string pnc_danger_before2 { get; set; }

        [Display(Name = "pnc_danger_before3", ResourceType = typeof(Resource))]
        public string pnc_danger_before3 { get; set; }

        [Display(Name = "pnc_danger_before4", ResourceType = typeof(Resource))]
        public string pnc_danger_before4 { get; set; }

        [Display(Name = "pnc_danger_before5", ResourceType = typeof(Resource))]
        public string pnc_danger_before5 { get; set; }


        [Display(Name = "pnc_danger_after1", ResourceType = typeof(Resource))]
        public string pnc_danger_after1 { get; set; }

        [Display(Name = "pnc_danger_after2", ResourceType = typeof(Resource))]
        public string pnc_danger_after2 { get; set; }

        [Display(Name = "pnc_danger_after3", ResourceType = typeof(Resource))]
        public string pnc_danger_after3 { get; set; }

        [Display(Name = "pnc_danger_after4", ResourceType = typeof(Resource))]
        public string pnc_danger_after4 { get; set; }

        [Display(Name = "pnc_danger_after5", ResourceType = typeof(Resource))]
        public string pnc_danger_after5 { get; set; }

        [Display(Name = "pnc_danger_after6", ResourceType = typeof(Resource))]
        public string pnc_danger_after6 { get; set; }

        [Display(Name = "pnc_danger_after7", ResourceType = typeof(Resource))]
        public string pnc_danger_after7 { get; set; }

        [Display(Name = "pnc_counseling1", ResourceType = typeof(Resource))]
        public string pnc_counseling1 { get; set; }

        [Display(Name = "pnc_counseling2", ResourceType = typeof(Resource))]
        public string pnc_counseling2 { get; set; }

        [Display(Name = "pnc_counseling3", ResourceType = typeof(Resource))]
        public string pnc_counseling3 { get; set; }

        [Display(Name = "pnc_counseling4", ResourceType = typeof(Resource))]
        public string pnc_counseling4 { get; set; }

        [Display(Name = "pnc_counseling5", ResourceType = typeof(Resource))]
        public string pnc_counseling5 { get; set; }

        [Display(Name = "pnc_counseling6", ResourceType = typeof(Resource))]
        public string pnc_counseling6 { get; set; }

        [Display(Name = "pnc_counseling7", ResourceType = typeof(Resource))]
        public string pnc_counseling7 { get; set; }

        [Display(Name = "pnc_counseling8", ResourceType = typeof(Resource))]
        public string pnc_counseling8 { get; set; }

        [Display(Name = "pnc_counseling9", ResourceType = typeof(Resource))]
        public string pnc_counseling9 { get; set; }

        [Display(Name = "pnc_counseling10", ResourceType = typeof(Resource))]
        public string pnc_counseling10 { get; set; }



        [Display(Name = "pnc_newborn_counseling1", ResourceType = typeof(Resource))]
        public string pnc_newborn_counseling1 { get; set; }

        [Display(Name = "pnc_newborn_counseling2", ResourceType = typeof(Resource))]
        public string pnc_newborn_counseling2 { get; set; }

        [Display(Name = "pnc_newborn_counseling3", ResourceType = typeof(Resource))]
        public string pnc_newborn_counseling3 { get; set; }

        [Display(Name = "pnc_newborn_counseling4", ResourceType = typeof(Resource))]
        public string pnc_newborn_counseling4 { get; set; }

        [Display(Name = "pnc_newborn_counseling5", ResourceType = typeof(Resource))]
        public string pnc_newborn_counseling5 { get; set; }

        [Display(Name = "pnc_newborn_counseling6", ResourceType = typeof(Resource))]
        public string pnc_newborn_counseling6 { get; set; }

        [Display(Name = "pnc_newborn_counseling7", ResourceType = typeof(Resource))]
        public string pnc_newborn_counseling7 { get; set; }

        [Display(Name = "pnc_newborn_counseling8", ResourceType = typeof(Resource))]
        public string pnc_newborn_counseling8 { get; set; }

        [Display(Name = "pnc_newborn_counseling9", ResourceType = typeof(Resource))]
        public string pnc_newborn_counseling9 { get; set; }

        [Display(Name = "pnc_support_1", ResourceType = typeof(Resource))]
        public string pnc_support_1 { get; set; }

        [Display(Name = "pnc_support_2", ResourceType = typeof(Resource))]
        public string pnc_support_2 { get; set; }

        [Display(Name = "pnc_support_3", ResourceType = typeof(Resource))]
        public string pnc_support_3 { get; set; }

        [Display(Name = "pnc_support_4", ResourceType = typeof(Resource))]
        public string pnc_support_4 { get; set; }




    }
}
