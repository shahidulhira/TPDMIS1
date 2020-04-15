using BDCO.Core.Command;
using BDCO.Domain.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BDCO.Domain.Aggregates
{
    [Table("DocAndRecordChecklist")]
    public class DocAndRecordChecklist : BaseModel
    {
        public long RecordID { get; set; }
        public string ChecklistId { get; set; }
        public string ProfileId { get; set; }
        public string Name { get; set; }
        public string ObserverName { get; set; }
        public string ObservationDate { get; set; }
        public string Q2_1_1 { get; set; }
        public string Q2_4_1 { get; set; }
        public string Q2_4_2 { get; set; }
        public string Q4_2_3 { get; set; }
        public string Q4_4_2 { get; set; }
        public List<DocAndRecordChecklist> GetAll()
        {
            List<DocAndRecordChecklist> result = unitOfWork.Repositories<DocAndRecordChecklist>().GetAll().ToList();
            return result;
        }
        public List<DocAndRecordChecklist> GetAll(string sql)
        {
            List<DocAndRecordChecklist> result = new List<DocAndRecordChecklist>();
            result = unitOfWork.GenericRepositories<DocAndRecordChecklist>().GetRecordSet(sql).ToList();
            return result;
        }
        public CommandResult Save(DocAndRecordChecklist command)
        {
            try
            {
                string result = "";
                DocAndRecordChecklist docAndRecordChecklist = new DocAndRecordChecklist();
                Tools.CopyClass(docAndRecordChecklist, command);
                var IsExist = unitOfWork.GenericRepositories<DocAndRecordChecklist>().FindBy(x => x.ProfileId == command.ProfileId).FirstOrDefault();
                if (IsExist == null)
                {
                    docAndRecordChecklist.Status = 1;
                    unitOfWork.GenericRepositories<DocAndRecordChecklist>().Insert(docAndRecordChecklist);
                    result = unitOfWork.SaveChange();
                    LogEventStore(docAndRecordChecklist.RecordID.ToString(), "DocAndRecordChecklist", "Save");
                }
                else
                {
                    docAndRecordChecklist.RecordID = IsExist.RecordID;
                    docAndRecordChecklist.Status = 1;

                    unitOfWork.GenericRepositories<DocAndRecordChecklist>().Update(docAndRecordChecklist);
                    result = unitOfWork.SaveChange();
                    LogEventStore(docAndRecordChecklist.RecordID.ToString(), "DocAndRecordChecklist", "Update");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = docAndRecordChecklist.RecordID,
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
        public CommandResult SaveOrUpdate(DocAndRecordChecklist command)
        {
            try
            {
                long recordId = command.RecordID;
                long serverRecordID = command.RecordID;
                string result = "";
                DocAndRecordChecklist docAndRecordChecklist = new DocAndRecordChecklist();
                Tools.CopyClass(docAndRecordChecklist, command);
                var IsExist = unitOfWork.GenericRepositories<DocAndRecordChecklist>().FindBy(x => x.ProfileId == command.ProfileId).FirstOrDefault();
                if (IsExist == null)
                {
                    docAndRecordChecklist.Status = 1;
                    unitOfWork.GenericRepositories<DocAndRecordChecklist>().Insert(docAndRecordChecklist);
                    result = unitOfWork.SaveChange();
                    LogEventStore(docAndRecordChecklist.RecordID.ToString(), "DocAndRecordChecklist", "Save");
                }
                else
                {
                    docAndRecordChecklist.RecordID = IsExist.RecordID;
                    docAndRecordChecklist.Status = 1;

                    unitOfWork.GenericRepositories<DocAndRecordChecklist>().Update(docAndRecordChecklist);
                    result = unitOfWork.SaveChange();
                    LogEventStore(docAndRecordChecklist.RecordID.ToString(), "DocAndRecordChecklist", "Update");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = docAndRecordChecklist.RecordID,
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
    }
    
    public class DocAndRecordChecklistResults
    {
        public string ProfileId { get; set; }
        public string ChecklistId { get; set; }
        public string QuestionText { get; set; }
        public string Marks { get; set; }
    }
}
