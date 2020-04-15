using BDCO.Domain;
using BDCO.Domain.Aggregates;
using BDCO.Domain.Models;
using BDCO.Web.Utility.JGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BDCO.Web.Controllers
{
    public class FilterController : Controller
    {
        readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public JsonResult GetUsers(string userName)
        {
            try
            {
                UserInfoViewModel obj = new UserInfoViewModel();
                var lst = obj.GetUserInfoForDropdownList(userName);
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetDesignation()
        {
            try
            {
                string sql = "SELECT CAST(DesignationId AS nvarchar) as Value, DesignationName as Text FROM DesignationInfo";
                var lst = _unitOfWork.RawSqlQuery<ValueText>(sql).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetBenUsers(string userName)
        {
            try
            {
                //UserInfoViewModel obj = new UserInfoViewModel();
                string sql = "SELECT  UserId,UserName, CAST(UserId as nvarchar(max)) as 'Value' ,userName + ' '+ FullName as 'Text',FullName  FROM AspNetUsers WHERE IsActive='1' AND UserId IN (SELECT UserId From MemberInfo)";
                var lst = _unitOfWork.GenericRepositories<UserInfoViewModel>().GetRecordSet(sql).ToList(); //
                //obj.GetUserInfoForDropdownList(userName);
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetCampId(string DistrictCode, string UpazilaCode, string UnionCode, string VillageCode)
        {
            try
            {
                string sql = string.Format($@"SELECT * FROM CampInfo where DistrictCode='{DistrictCode}'AND UpazilaCode='{UpazilaCode}' AND UnionCode='{UnionCode}' AND VillageCode='{VillageCode}'");
                var lst = _unitOfWork.GenericRepositories<CampInfo>().GetRecordSet(sql).Select(x => new
                {
                    Value = x.CampId,
                    Text = x.CampName
                }).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetCenterId(string DistrictCode, string UpazilaCode, string UnionCode, string VillageCode)
        {
            try
            {
                string sql = string.Format($@"SELECT * FROM CenterInfo where DistrictCode='{DistrictCode}'AND UpazilaCode='{UpazilaCode}' AND UnionCode='{UnionCode}' AND VillageCode='{VillageCode}'");
                var lst = _unitOfWork.GenericRepositories<CenterInfo>().GetRecordSet(sql).ToList().Select(x => new
                {
                    Value = x.CenterId,
                    Text = x.CenterName
                }).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDistrictCode()
        {
            try
            {
                string sql = string.Format(@"SELECT distinct d.* FROM GeoLocation.dbo.District d, GeoLocation rg WHERE d.DistrictCode=rg.DistrictCode ORDER BY [DistrictName]");
                var lst = _unitOfWork.GenericRepositories<Districts>().GetRecordSet(sql).Select(x => new
                {
                    Value = x.DistrictCode,
                    Text = x.DistrictName
                }).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetUpazilaCode(string DistrictCode)
        {
            try
            {
                string sql = string.Format(@"SELECT distinct d.* FROM GeoLocation.dbo.Upazila d, GeoLocation rg WHERE (d.DistrictCode=rg.DistrictCode AND d.UpazilaCode=rg.UpazilaCode) AND  d.DistrictCode = '" + DistrictCode + "' ORDER BY [UpazilaName]");
                var lst = _unitOfWork.GenericRepositories<Upazila>().GetRecordSet(sql).Select(x => new
                {
                    Value = x.UpazilaCode,
                    Text = x.UpazilaName
                }).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetUnionCode(string DistrictCode, string UpazilaCode)
        {
            try
            {
                string sql = string.Format(@"SELECT distinct d.* FROM GeoLocation.dbo.Unions  d, GeoLocation rg WHERE(d.DistrictCode = rg.DistrictCode AND d.UpazilaCode = rg.UpazilaCode AND d.UnionCode = rg.UnionCode) AND  d.DistrictCode = '" + DistrictCode
                    + "' AND d.UpazilaCode = '" + UpazilaCode + "' ORDER BY [UnionName]");
                var lst = _unitOfWork.GenericRepositories<Unions>().GetRecordSet(sql).Select(x => new
                {
                    Value = x.UnionCode,
                    Text = x.UnionName
                }).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetVillageCode(string DistrictCode, string UpazilaCode, string UnionCode)
        {
            try
            {
                string sql = string.Format(@"SELECT distinct d.* FROM GeoLocation.dbo.Village  d, GeoLocation rg WHERE (d.DistrictCode=rg.DistrictCode AND d.UpazilaCode=rg.UpazilaCode AND d.UnionCode=rg.UnionCode AND d.VillageCode=rg.VillageCode) AND  d.DistrictCode = '" + DistrictCode
                    + "' AND d.UpazilaCode = '" + UpazilaCode + "' AND d.UnionCode = '" + UnionCode + "' ORDER BY [VillageName]");
                var lst = _unitOfWork.GenericRepositories<Village>().GetRecordSet(sql).Select(x => new
                {
                    Value = x.VillageCode,
                    Text = x.VillageName
                }).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetServicePoint()
        {
            try
            {
                var lst = _unitOfWork.GenericRepositories<ServicePoint>().GetAll().Where(r => r.IsActive == 1).Select(x => new
                {
                    Value = x.ServicePointId,
                    Text = x.ServicePointName
                }).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetBlock()
        {
            try
            {
                string sql = "SELECT BlockInfo.BlockId, BlockInfo.BlockName BlockName,(BlockInfo.BlockName+'('+CampInfo.CampName+')') BlockNameWithCamp FROM BlockInfo INNER JOIN CampInfo ON CampInfo.CampId = BlockInfo.CampId";
                var lst = _unitOfWork.RawSqlQuery<BlockDropdownWithCamp>(sql).Select(x => new
                {
                    Value = x.BlockId,
                    Text = x.BlockNameWithCamp
                }).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetBlockId(string CampId)
        {
            try
            {
                string sql = $@"SELECT BlockId, BlockName,(BlockName+' ('+CampName+')') BlockNameWithCamp 
                                FROM BlockInfo 
                                INNER JOIN CampInfo ON CampInfo.CampId = BlockInfo.CampId
                                WHERE BlockInfo.CampId='{CampId}'";
                var lst = _unitOfWork.RawSqlQuery<BlockDropdownWithCamp>(sql).Select(x => new
                {
                    Value = x.BlockId,
                    Text = x.BlockNameWithCamp
                }).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAdmissionType()
        {
            try
            {
                string sql = "SELECT ReferenceCode as Value, ReferenceText as Text FROM ItemReference WHERE TableName = 'GmpDetails' AND ColumnName = 'AdmissionType'";
                var lst = _unitOfWork.RawSqlQuery<ValueText>(sql).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
               

        public JsonResult GetTeacherSubjects()
        {
            try
            {
                string sql = "SELECT CAST(SubjectId AS nvarchar) as Value, SubjectName as Text FROM TeacherSubjects";
                var lst = _unitOfWork.RawSqlQuery<ValueText>(sql).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetEducationQualifications()
        {
            try
            {
                string sql = "SELECT CAST(QualificationId AS nvarchar) as Value, QualificationName as Text FROM EducationQualifications";
                var lst = _unitOfWork.RawSqlQuery<ValueText>(sql).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPartnerInfo()
        {
            try
            {
                string sql = "SELECT PartnerId as Value, PartnerName as Text FROM PartnerInfo";
                var lst = _unitOfWork.RawSqlQuery<ValueText>(sql).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetProfileInfo()
        {
            try
            {
                string sql = "SELECT ProfileId as Value, Name as Text FROM ProfileInfo";
                var lst = _unitOfWork.RawSqlQuery<ValueText>(sql).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetLearningFacility()
        {
            try
            {
                string sql = "SELECT FacilityId as Value, FacilityName as Text FROM LearningFacility";
                var lst = _unitOfWork.RawSqlQuery<ValueText>(sql).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetTrainingType()
        {
            try
            {
                string sql = "SELECT DISTINCT TypeId as Value, TypeName as Text FROM TrainingCategory";
                var lst = _unitOfWork.RawSqlQuery<ValueText>(sql).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetTrainingCategory(string typeid)
        {
            try
            {
                string sql = "SELECT CategoryId as Value, CategoryName as Text FROM TrainingCategory WHERE TypeId='"+ typeid + "'";
                var lst = _unitOfWork.RawSqlQuery<ValueText>(sql).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
                
        public JsonResult GetTrainingTopic(string typeid)
        {
            try
            {
                string sql = "SELECT TopicId as Value, TopicName as Text FROM TrainingTopic WHERE typeid='" + typeid + "'";
                var lst = _unitOfWork.RawSqlQuery<ValueText>(sql).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetTrainingMaterial(string typeid, string categoryId)
        {
            try
            {
                string sql = "SELECT MaterialId as Value, MaterialName as Text FROM TrainingMaterial WHERE typeid='"+ typeid + "' AND CategoryId='"+ categoryId + "'";
                var lst = _unitOfWork.RawSqlQuery<ValueText>(sql).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        } 

        public JsonResult GetBatchIndex(string typeid, string categoryId)
        {
            try
            {
                string sql = "SELECT BatchCode as Value, BatchName as Text FROM BatchIndex WHERE typeid='" + typeid + "' AND CategoryId='" + categoryId + "'";
                var lst = _unitOfWork.RawSqlQuery<ValueText>(sql).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }

    public class BlockDropdownWithCamp
    {
        public int? BlockId { get; set; }
        public string BlockName { get; set; }
        public string BlockNameWithCamp { get; set; }
    }

    public class UserInfoViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string StaffID { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public string UserInfo
        {
            get
            {
                return UserName + "-" + FullName;
            }
        }

        UnitOfWork _unitOfWork = new UnitOfWork();
        public List<UserInfoViewModel> GetUserInfoForDropdownList(string userName)
        {
            List<UserInfoViewModel> result = new List<UserInfoViewModel>();
            string sqlQuery = string.Format("SELECT  UserId,UserName, CAST(UserId as nvarchar(max)) as 'Value' ,userName + ' '+ FullName as 'Text',FullName  FROM AspNetUsers");
            result = _unitOfWork.GenericRepositories<UserInfoViewModel>().GetRecordSet(sqlQuery).ToList();
            return result;
        }


    }
    public class ValueText
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}