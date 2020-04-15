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
    [Table("PartnerInfo")]
    public class PartnerInfo: BaseModel
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordID { get; set; }
        public long? PartnerId { get; set; }
        public string PartnerName { get; set; }
        public string Address { get; set; }
        public string FacilityStatus { get; set; }
        public int? CampId { get; set; }
        public int? BlockId { get; set; }
        public int? HostCommunityActivities { get; set; }
        

        public CommandResult Save(PartnerInfo command)
        {
            try
            {
                string result = "";
                PartnerInfo teacherPartner = new PartnerInfo();
                Tools.CopyClass(teacherPartner, command);
                var IsExist = unitOfWork.GenericRepositories<PartnerInfo>().FindBy(x => x.RecordID == command.RecordID).FirstOrDefault();
                if (IsExist == null)
                {
                    teacherPartner.Status = 1;
                    unitOfWork.GenericRepositories<PartnerInfo>().Insert(teacherPartner);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherPartner.RecordID.ToString(), "PartnerInfo", "Save");
                }
                else
                {
                    teacherPartner.RecordID = IsExist.RecordID;
                    teacherPartner.Status = 1;

                    unitOfWork.GenericRepositories<PartnerInfo>().Update(teacherPartner);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherPartner.RecordID.ToString(), "PartnerInfo", "Update");
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

        public List<PartnerInfo> GetAllPartner()
        {
            List<PartnerInfo> result = unitOfWork.Repositories<PartnerInfo>().GetAll().ToList();
            return result;
        }
        public List<PartnerInfo> GetAllPartner(string sql)
        {
            List<PartnerInfo> result = new List<PartnerInfo>();
            result = unitOfWork.GenericRepositories<PartnerInfo>().GetRecordSet(sql).ToList();
            return result;
        }

        public CommandResult Delete(PartnerInfo command)
        {
            try
            {
                string result = "";
                PartnerInfo teacherPartner = new PartnerInfo();
                Tools.CopyClass(teacherPartner, command);
                var IsExist = unitOfWork.GenericRepositories<PartnerInfo>().FindBy(x => x.RecordID == command.RecordID).FirstOrDefault();
                if (IsExist != null)
                {
                    unitOfWork.GenericRepositories<PartnerInfo>().Delete(teacherPartner);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherPartner.RecordID.ToString(), "PartnerInfo", "Delete");
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
        public CommandResult SaveOrUpdate(PartnerInfo command)
        {
            try
            {
                long recordId = command.RecordID;
                long serverRecordID = command.RecordID;
                string result = "";
                PartnerInfo teacherPartner = new PartnerInfo();
                Tools.CopyClass(teacherPartner, command);
                var IsExist = unitOfWork.GenericRepositories<PartnerInfo>().FindBy(x => x.PartnerId == command.PartnerId).FirstOrDefault();
                if (IsExist == null)
                {
                    teacherPartner.Status = 1;
                    unitOfWork.GenericRepositories<PartnerInfo>().Insert(teacherPartner);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherPartner.RecordID.ToString(), "PartnerInfo", "Save");
                }
                else
                {
                    teacherPartner.RecordID = IsExist.RecordID;
                    teacherPartner.Status = 1;

                    unitOfWork.GenericRepositories<PartnerInfo>().Update(teacherPartner);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherPartner.RecordID.ToString(), "PartnerInfo", "Update");
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

        public List<PartnerInfoVM> GetAllPartnerInfo(PartnerInfoFilter block)
        {
            string sql = $"EXEC SearchPartnerInfo '{block.DistrictCode}','{block.UpazilaCode}','{block.UnionCode}','{block.VillageCode}','{block.CampId}','{block.CenterId}','{block.BlockId}','{block.PageNo}'";
            var result = unitOfWork.RawSqlQuery<PartnerInfoVM>(sql).ToList();
            return result;
        }
        public PartnerInfo GetPartnerInfoByID(int recordId)
        {
            return unitOfWork.GenericRepositories<PartnerInfo>().FindBy(x => x.RecordID == recordId).FirstOrDefault();
        }
    }

    public class PartnerInfoFilter
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

    public class PartnerInfoVM
    {
        public long RecordID { get; set; }
        public long PartnerId { get; set; }
        public string PartnerName { get; set; }
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
