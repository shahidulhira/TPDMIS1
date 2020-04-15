using BDCO.Core.Command;
using BDCO.Domain.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BDCO.Domain.Aggregates
{
    [Table("ProfileInfo")]
    public class ProfileInfo : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordID { get; set; }
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

        public List<ProfileInfo> GetAllProfile()
        {
            List<ProfileInfo> result = unitOfWork.Repositories<ProfileInfo>().GetAll().ToList();
            return result;
        }
        public List<ProfileInfo> GetAllProfile(string sql)
        {
            List<ProfileInfo> result = new List<ProfileInfo>();
            result = unitOfWork.GenericRepositories<ProfileInfo>().GetRecordSet(sql).ToList();
            return result;
        }
        public CommandResult Save(ProfileInfo command)
        {
            try
            {
                string result = "";
                ProfileInfo teacherProfile = new ProfileInfo();
                Tools.CopyClass(teacherProfile, command);
                var IsExist = unitOfWork.GenericRepositories<ProfileInfo>().FindBy(x => x.ProfileId == command.ProfileId).FirstOrDefault();
                if (IsExist == null)
                {
                    teacherProfile.Status = 1;
                    unitOfWork.GenericRepositories<ProfileInfo>().Insert(teacherProfile);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherProfile.RecordID.ToString(), "TeacherProfile", "Save");
                }
                else
                {
                    teacherProfile.RecordID = IsExist.RecordID;
                    teacherProfile.Status = 1;

                    unitOfWork.GenericRepositories<ProfileInfo>().Update(teacherProfile);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherProfile.RecordID.ToString(), "TeacherProfile", "Update");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = teacherProfile.RecordID,
                    RecordId = command.RecordID
                };
            }
            catch (Exception ex)
            {
                return new CommandResult()
                {
                    Success = false,
                    Status = 400,
                    Message = ex.Message,
                    RecordId = command.RecordID
                };
            }
        }
        public CommandResult Delete(ProfileInfo command)
        {
            try
            {
                string result = "";
                ProfileInfo teacherProfile = new ProfileInfo();
                Tools.CopyClass(teacherProfile, command);
                var IsExist = unitOfWork.GenericRepositories<ProfileInfo>().FindBy(x => x.RecordID == command.RecordID).FirstOrDefault();
                if (IsExist != null)
                {
                    unitOfWork.GenericRepositories<ProfileInfo>().Delete(teacherProfile);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherProfile.RecordID.ToString(), "TeacherProfile", "Delete");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = teacherProfile.RecordID,
                    RecordId = command.RecordID
                };
            }
            catch (Exception ex)
            {
                return new CommandResult()
                {
                    Success = false,
                    Status = 400,
                    Message = ex.Message,
                    RecordId = command.RecordID
                };
            }
        }
        //This Method is Written of Insert and Update Data of API --> Start
        public CommandResult SaveOrUpdate(ProfileInfo command)
        {
            try
            {
                long recordId = command.RecordID;
                long serverRecordID = command.RecordID;
                string result = "";
                ProfileInfo teacherProfile = new ProfileInfo();
                Tools.CopyClass(teacherProfile, command);
                var IsExist = unitOfWork.GenericRepositories<ProfileInfo>().FindBy(x => x.ProfileId == command.ProfileId).FirstOrDefault();
                if (IsExist == null)
                {
                    teacherProfile.Status = 1;
                    unitOfWork.GenericRepositories<ProfileInfo>().Insert(teacherProfile);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherProfile.RecordID.ToString(), "TeacherProfile", "Save");
                }
                else
                {
                    teacherProfile.RecordID = IsExist.RecordID;
                    teacherProfile.Status = 1;

                    unitOfWork.GenericRepositories<ProfileInfo>().Update(teacherProfile);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherProfile.RecordID.ToString(), "TeacherProfile", "Update");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = teacherProfile.RecordID,
                    RecordId = RecordID
                };
            }
            catch (Exception ex)
            {
                return new CommandResult()
                {
                    Success = false,
                    Status = 400,
                    Message = ex.Message,
                    RecordId = command.RecordID
                };
            }
        }
        public List<ProfileInfoVM> GetAllProfileInfo(ProfileInfoFilter block)
        {
            string sql = $"EXEC SearchProfileInfo '{block.DistrictCode}','{block.UpazilaCode}','{block.UnionCode}','{block.VillageCode}','{block.CampId}','{block.CenterId}','{block.BlockId}','{block.TypeId}' , '{block.PageNo}'";
            var result = unitOfWork.RawSqlQuery<ProfileInfoVM>(sql).ToList();
            return result;
        }
        public ProfileInfo GetProfileInfoByID(int recordId)
        {
            return unitOfWork.GenericRepositories<ProfileInfo>().FindBy(x => x.RecordID == recordId).FirstOrDefault();
        }
    }
    public class ProfileInfoFilter
    {
        public string BlockId { get; set; }
        public string BlockName { get; set; }
        public string CampId { get; set; }
        public int CenterId { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string CampName { get; set; }
        public string CenterName { get; set; }
        public string TypeId { get; set; }
        public int? TotalRecord { get; set; }
        public int? PageNo { get; set; }
    }

    public class ProfileInfoVM
    {
        public long RecordID { get; set; }
        public string ProfileId { get; set; }
        public string TypeId { get; set; }
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
        public string DistrictName { get; set; }
        public string UpazilaName { get; set; }
        public string UnionName { get; set; }
        public string VillageName { get; set; }
        public string CampName { get; set; }
        public string CenterName { get; set; }
        public string BlockName { get; set; }
        public int TotalRecord { get; set; }
    }
}
