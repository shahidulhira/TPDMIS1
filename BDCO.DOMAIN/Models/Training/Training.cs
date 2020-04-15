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
    [Table("TrainingInfo")]
    public class TrainingInfo : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordID { get; set; }
        [Key]
        public string TrainingId { get; set; }
        public string TrainingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TypeId { get; set; }
        public string CategoryId { get; set; }
        public string TopicOthers { get; set; }
        public string Venue { get; set; }
        public string Trainer { get; set; }
        public string BatchCode { get; set; }
        public string Remarks { get; set; }

        public List<TrainingInfoMaterial> trainingInfoMaterial { get; set; }
        public List<TrainingInfoTopic> trainingInfoTopic { get; set; }

       
        public CommandResult Save(TrainingInfo command, List<TrainingInfoMaterial> materials, List<TrainingInfoTopic> topics)
        {
            try
            {
                string result = "";
                TrainingInfo trainingInfo = new TrainingInfo();
                Tools.CopyClass(trainingInfo, command);
                var IsExist = unitOfWork.GenericRepositories<TrainingInfo>().FindBy(x => x.RecordID == command.RecordID).FirstOrDefault();
                if (IsExist == null)
                {
                    trainingInfo.Status = 1;
                    trainingInfo.TrainingId = "1";
                    unitOfWork.GenericRepositories<TrainingInfo>().Insert(trainingInfo);
                    result = unitOfWork.SaveChange();

                    var IsExists = unitOfWork.GenericRepositories<TrainingInfo>().FindBy(x => x.TrainingId == command.TrainingId).FirstOrDefault();

                    foreach (var item in topics)
                    {
                        string sql = "SELECT * FROM TrainingTopic WHERE TopicId='" + item.TopicId + "'";
                        var topic = unitOfWork.GetDataTable(sql);

                        TrainingInfoTopic topicinfo = new TrainingInfoTopic();
                        Tools.CopyClass(topicinfo, item);
                        topicinfo.TrainingId = trainingInfo.TrainingId;
                        topicinfo.TopicType = topic.Rows[0]["TopicType"].ToString();

                        unitOfWork.GenericRepositories<TrainingInfoTopic>().Insert(topicinfo);
                    }


                    foreach (var item in materials)
                    {

                        string sql = "SELECT * FROM TrainingMaterial WHERE MaterialId='" + item.MaterialId + "'";
                        var material = unitOfWork.GetDataTable(sql);

                        TrainingInfoMaterial materialinfo = new TrainingInfoMaterial();
                        Tools.CopyClass(materialinfo, item);
                        materialinfo.TrainingId = trainingInfo.TrainingId;
                        materialinfo.MaterialType = material.Rows[0]["MaterialType"].ToString();

                        unitOfWork.GenericRepositories<TrainingInfoMaterial>().Insert(materialinfo);

                    }

                    result = unitOfWork.SaveChange();

                    LogEventStore(trainingInfo.RecordID.ToString(), "TrainingInfo", "Save");
                }
                else
                {
                    trainingInfo.RecordID = IsExist.RecordID;
                    trainingInfo.Status = 1;

                    unitOfWork.GenericRepositories<TrainingInfo>().Update(trainingInfo);

                    unitOfWork.GenericRepositories<TrainingInfoTopic>().Delete(trainingInfo.TrainingId);
                    unitOfWork.GenericRepositories<TrainingInfoMaterial>().Delete(trainingInfo.TrainingId);

                    foreach (var item in topics)
                    {
                        string sql = "SELECT * FROM TrainingTopic WHERE TopicId='" + item.TopicId + "'";
                        var topic = unitOfWork.GetDataTable(sql);

                        TrainingInfoTopic topicinfo = new TrainingInfoTopic();
                        Tools.CopyClass(topicinfo, item);
                        topicinfo.TrainingId = trainingInfo.TrainingId;
                        topicinfo.TopicType = topic.Rows[0]["TopicType"].ToString();

                        unitOfWork.GenericRepositories<TrainingInfoTopic>().Insert(topicinfo);
                    }


                    foreach (var item in materials)
                    {

                        string sql = "SELECT * FROM TrainingMaterial WHERE MaterialId='" + item.MaterialId + "'";
                        var material = unitOfWork.GetDataTable(sql);

                        TrainingInfoMaterial materialinfo = new TrainingInfoMaterial();
                        Tools.CopyClass(materialinfo, item);
                        materialinfo.TrainingId = trainingInfo.TrainingId;
                        materialinfo.MaterialType = material.Rows[0]["MaterialType"].ToString();

                        unitOfWork.GenericRepositories<TrainingInfoMaterial>().Insert(materialinfo);

                    }

                    result = unitOfWork.SaveChange();
                    LogEventStore(trainingInfo.RecordID.ToString(), "TrainingInfo", "Update");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = trainingInfo.RecordID,
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
        public CommandResult Delete(TrainingInfo command)
        {
            try
            {
                string result = "";
                TrainingInfo teacherProfile = new TrainingInfo();
                Tools.CopyClass(teacherProfile, command);
                var IsExist = unitOfWork.GenericRepositories<TrainingInfo>().FindBy(x => x.RecordID == command.RecordID).FirstOrDefault();
                if (IsExist != null)
                {
                    var IsExistsTopic = unitOfWork.GenericRepositories<TrainingInfoTopic>().FindBy(x => x.TrainingId == IsExist.TrainingId).ToList();
                    var IsExistsMaterial = unitOfWork.GenericRepositories<TrainingInfoMaterial>().FindBy(x => x.TrainingId == IsExist.TrainingId).ToList();

                    unitOfWork.GenericRepositories<TrainingInfoTopic>().DeleteAll(IsExistsTopic);
                    unitOfWork.GenericRepositories<TrainingInfoMaterial>().DeleteAll(IsExistsMaterial);
                    unitOfWork.GenericRepositories<TrainingInfo>().Delete(teacherProfile);
                    result = unitOfWork.SaveChange();
                    LogEventStore(teacherProfile.RecordID.ToString(), "TrainingInfo", "Delete");
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
        public List<TrainingInfoVM> GetAllTrainingInfo(ProfileInfoFilter block)
        {
            string sql = $"EXEC SearchTrainingInfo '{block.DistrictCode}','{block.UpazilaCode}','{block.UnionCode}','{block.VillageCode}','{block.CenterId}','{block.PageNo}'";
            var result = unitOfWork.RawSqlQuery<TrainingInfoVM>(sql).ToList();
            return result;
        }
        public TrainingInfo GetTrainingInfoByID(string trainingId)
        {
            var result= unitOfWork.GenericRepositories<TrainingInfo>().FindBy(x => x.TrainingId == trainingId).FirstOrDefault();

            //result.trainingInfoMaterial = unitOfWork.GenericRepositories<TrainingInfoMaterial>().FindBy(x => x.TrainingId == trainingId).ToList();

            result.trainingInfoMaterial = unitOfWork.GenericRepositories<TrainingInfoMaterial>().GetRecordSet("SELECT * FROM TrainingInfoMaterial WHERE TrainingId = '" + trainingId + "'").ToList();
            result.trainingInfoTopic = unitOfWork.GenericRepositories<TrainingInfoTopic>().GetRecordSet("SELECT * FROM TrainingInfoTopic WHERE TrainingId = '"+trainingId+"'").ToList();



            return result;
        }
    }
    public class TrainingInfoVM
    {
        [Key]
        public long RecordID { get; set; }
        public string TrainingId { get; set; }
        public string TrainingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Venue { get; set; }
        public string Trainer { get; set; }
        public string DistrictName { get; set; }
        public string UpazilaName { get; set; }
        public string UnionName { get; set; }
        public string VillageName { get; set; }
        public string CenterName { get; set; }
        public string DistrictCode { get; set; }
        public string CenterCode { get; set; }
        public string UnionCode { get; set; }
        public string UpazilaCode { get; set; }
        public string VillageCode { get; set; }
        public int TotalRecord { get; set; }
    }
    [Table("TrainingCategory")]
    public class TrainingCategory : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long RecordID { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeNameEn { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameEn { get; set; }
    }
    public class TrainingTopic
    {
        public int RecordId { get; set; }
        public string TopicId { get; set; }
        public string TopicName { get; set; }
        public string TopicNameEn { get; set; }
        public string TopicType { get; set; }
        public string TypeId { get; set; }
        public string CategoryId { get; set; }
    }
    public class TrainingMaterial
    {
        public long RecordID { get; set; }
        public string MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialNameEn { get; set; }
        public string CategoryId { get; set; }
        public string TypeId { get; set; }
        public string MaterialType { get; set; }
    }
    [Table("TrainingInfoMaterial")]
    public class TrainingInfoMaterial
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long? RowId { get; set; }        
        public string TrainingId { get; set; }
        public string MaterialId { get; set; }
        public string MaterialType { get; set; }
    }
    [Table("TrainingInfoTopic")]
    public class TrainingInfoTopic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long? RowId { get; set; }       
        public string TrainingId { get; set; }
        public string TopicId { get; set; }
        public string TopicType { get; set; }
    }
    [Table("TrainingMemberInfo")]
    public class TrainingMemberInfo
    {
        [Key]
        public long? RowId { get; set; }
        public string TrainingId { get; set; }
        public string ProfileId { get; set; }
        public string Name { get; set; }
        public string UUID { get; set; }
        public string PresenceType { get; set; }
        public int? Present { get; set; }
        public string AttendanceRemarks { get; set; }
    }

}
