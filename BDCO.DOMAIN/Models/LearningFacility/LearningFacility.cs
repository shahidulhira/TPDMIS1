using BDCO.Core.Command;
using BDCO.Domain.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates
{

    [Table("LearningFacility")]
    public class LearningFacility : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordID { get; set; }
        public long? FacilityId { get; set; }
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



        public CommandResult Save(LearningFacility command)
        {
            try
            {
                string result = "";
                LearningFacility teacherProfile = new LearningFacility();
                Tools.CopyClass(teacherProfile, command);
                var IsExist = unitOfWork.GenericRepositories<LearningFacility>().FindBy(x => x.RecordID == command.RecordID).FirstOrDefault();
                if (IsExist == null)
                {
                    teacherProfile.Status = 1;
                    unitOfWork.GenericRepositories<LearningFacility>().Insert(teacherProfile);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherProfile.RecordID.ToString(), "TeacherProfile", "Save");
                }
                else
                {
                    teacherProfile.RecordID = IsExist.RecordID;
                    teacherProfile.Status = 1;

                    unitOfWork.GenericRepositories<LearningFacility>().Update(teacherProfile);
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


        public List<LearningFacility> GetAllPartner()
        {
            List<LearningFacility> result = unitOfWork.Repositories<LearningFacility>().GetAll().ToList();
            return result;
        }
        public List<LearningFacility> GetAllPartner(string sql)
        {
            List<LearningFacility> result = new List<LearningFacility>();
            result = unitOfWork.GenericRepositories<LearningFacility>().GetRecordSet(sql).ToList();
            return result;
        }

        public CommandResult Delete(LearningFacility command)
        {
            try
            {
                string result = "";
                LearningFacility teacherPartner = new LearningFacility();
                Tools.CopyClass(teacherPartner, command);
                var IsExist = unitOfWork.GenericRepositories<LearningFacility>().FindBy(x => x.RecordID == command.RecordID).FirstOrDefault();
                if (IsExist != null)
                {
                    unitOfWork.GenericRepositories<LearningFacility>().Delete(teacherPartner);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherPartner.RecordID.ToString(), "LearningFacility", "Delete");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = teacherPartner.RecordID,
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
        public CommandResult SaveOrUpdate(LearningFacility command)
        {
            try
            {
                long recordId = command.RecordID;
                long serverRecordID = command.RecordID;
                string result = "";
                LearningFacility teacherPartner = new LearningFacility();
                Tools.CopyClass(teacherPartner, command);
                var IsExist = unitOfWork.GenericRepositories<LearningFacility>().FindBy(x => x.PartnerId == command.PartnerId).FirstOrDefault();
                if (IsExist == null)
                {
                    teacherPartner.Status = 1;
                    unitOfWork.GenericRepositories<LearningFacility>().Insert(teacherPartner);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherPartner.RecordID.ToString(), "LearningFacility", "Save");
                }
                else
                {
                    teacherPartner.RecordID = IsExist.RecordID;
                    teacherPartner.Status = 1;

                    unitOfWork.GenericRepositories<LearningFacility>().Update(teacherPartner);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherPartner.RecordID.ToString(), "LearningFacility", "Update");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = teacherPartner.RecordID,
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

        public List<LearningFacilityVM> GetAllLearningFacility(LearningFacilityFilter block)
        {
            string sql = $"EXEC SearchLearningFacility '{block.DistrictCode}','{block.UpazilaCode}','{block.UnionCode}','{block.VillageCode}','{block.CampId}','{block.CenterId}','{block.BlockId}','{block.PageNo}'";
            var result = unitOfWork.RawSqlQuery<LearningFacilityVM>(sql).ToList();
            return result;
        }
        public LearningFacility GetLearningFacilityByID(int recordId)
        {
            return unitOfWork.GenericRepositories<LearningFacility>().FindBy(x => x.RecordID == recordId).FirstOrDefault();
        }
    }


    public class LearningFacilityFilter
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
        public int? TotalRecord { get; set; }
        public int? PageNo { get; set; }
    }

    public class LearningFacilityVM
    {
        public long RecordID { get; set; }
        public long FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string DistrictName { get; set; }
        public string UpazilaName { get; set; }
        public string UnionName { get; set; }
        public string VillageName { get; set; }
        public string CampName { get; set; }
        public string CenterName { get; set; }
        public string BlockName { get; set; }
        public int? BlockId { get; set; }
        public int? CampId { get; set; }
        public string DistrictCode { get; set; }
        public string CenterCode { get; set; }
        public string UnionCode { get; set; }
        public string UpazilaCode { get; set; }
        public string VillageCode { get; set; }
        public int TotalRecord { get; set; }

    }
}
