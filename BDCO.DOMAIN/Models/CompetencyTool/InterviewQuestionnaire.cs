using BDCO.Core.Command;
using BDCO.Domain.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BDCO.Domain.Aggregates
{
    [Table("InterviewQuestionnaire")]
    public class InterviewQuestionnaire : BaseModel
    {
        public long RecordID { get; set; }
        public string ChecklistId { get; set; }
        public string ProfileId { get; set; }
        public string Name { get; set; }
        public string ObserverName { get; set; }
        public string ObservationDate { get; set; }
        public string Q1_1_5 { get; set; }
        public string Q1_2_2 { get; set; }
        public string Q1_3_3 { get; set; }
        public string Q1_4_2 { get; set; }
        public string Q3_1_2 { get; set; }
        public string Q3_1_4 { get; set; }
        public string Q3_2_1 { get; set; }
        public string Q3_2_2 { get; set; }
        public string Q4_1_2 { get; set; }
        public string Q4_1_3 { get; set; }
        public string Q4_3_1 { get; set; }
        public string Q4_4_1 { get; set; }
        public string Q4_4_2 { get; set; }


        public List<InterviewQuestionnaire> GetAll()
        {
            List<InterviewQuestionnaire> result = unitOfWork.Repositories<InterviewQuestionnaire>().GetAll().ToList();
            return result;
        }
        public List<InterviewQuestionnaire> GetAll(string sql)
        {
            //List<InterviewQuestionnaire> result = new List<InterviewQuestionnaire>();
            //result = unitOfWork.GenericRepositories<InterviewQuestionnaire>().GetRecordSet(sql).ToList();
            //return result;
            return unitOfWork.GenericRepositories<InterviewQuestionnaire>().GetRecordSet(sql).ToList();
        }
        public CommandResult Save(InterviewQuestionnaire command)
        {
            try
            {
                string result = "";
                InterviewQuestionnaire interviewQuestionnaire = new InterviewQuestionnaire();
                Tools.CopyClass(interviewQuestionnaire, command);
                var IsExist = unitOfWork.GenericRepositories<InterviewQuestionnaire>().FindBy(x => x.ProfileId == command.ProfileId).FirstOrDefault();
                if (IsExist == null)
                {
                    interviewQuestionnaire.Status = 1;
                    unitOfWork.GenericRepositories<InterviewQuestionnaire>().Insert(interviewQuestionnaire);
                    result = unitOfWork.SaveChange();
                    LogEventStore(interviewQuestionnaire.RecordID.ToString(), "InterviewQuestionnaire", "Save");
                }
                else
                {
                    interviewQuestionnaire.RecordID = IsExist.RecordID;
                    interviewQuestionnaire.Status = 1;

                    unitOfWork.GenericRepositories<InterviewQuestionnaire>().Update(interviewQuestionnaire);
                    result = unitOfWork.SaveChange();
                    LogEventStore(interviewQuestionnaire.RecordID.ToString(), "InterviewQuestionnaire", "Update");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = interviewQuestionnaire.RecordID,
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
        public CommandResult SaveOrUpdate(InterviewQuestionnaire command)
        {
            try
            {
                long recordId = command.RecordID;
                long serverRecordID = command.RecordID;
                string result = "";
                InterviewQuestionnaire interviewQuestionnaire = new InterviewQuestionnaire();
                Tools.CopyClass(interviewQuestionnaire, command);
                var IsExist = unitOfWork.GenericRepositories<InterviewQuestionnaire>().FindBy(x => x.ProfileId == command.ProfileId).FirstOrDefault();
                if (IsExist == null)
                {
                    interviewQuestionnaire.Status = 1;
                    unitOfWork.GenericRepositories<InterviewQuestionnaire>().Insert(interviewQuestionnaire);
                    result = unitOfWork.SaveChange();
                    LogEventStore(interviewQuestionnaire.RecordID.ToString(), "InterviewQuestionnaire", "Save");
                }
                else
                {
                    interviewQuestionnaire.RecordID = IsExist.RecordID;
                    interviewQuestionnaire.Status = 1;

                    unitOfWork.GenericRepositories<InterviewQuestionnaire>().Update(interviewQuestionnaire);
                    result = unitOfWork.SaveChange();
                    LogEventStore(interviewQuestionnaire.RecordID.ToString(), "InterviewQuestionnaire", "Update");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = interviewQuestionnaire.RecordID,
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
        public List<InterviewFilter> FilterAll(InterviewFilter filter)
        {
            string sql = $"EXEC SearchPartnerInfo '{filter.DistrictCode}','{filter.UpazilaCode}','{filter.UnionCode}','{filter.VillageCode}','{filter.CenterCode}','{filter.PageNo}'";
            var result = unitOfWork.RawSqlQuery<InterviewFilter>(sql).ToList();
            return result;
        }
    }

    public class InterviewQuestionnaireResults
    {
        public string ProfileId { get; set; }
        public string ChecklistId { get; set; }
        public string QuestionText { get; set; }
        public string Marks { get; set; }
    }
    public class InterviewFilter
    {
        public long RecordID { get; set; }
        public string ChecklistId { get; set; }
        public string ProfileId { get; set; }
        public string DistrictCode { get; set; }
        public string CenterCode { get; set; }
        public string UnionCode { get; set; }
        public string UpazilaCode { get; set; }
        public string VillageCode { get; set; }
        public int? PageNo { get; set; }
        public int? TotalRecord { get; set; }
    }
}
