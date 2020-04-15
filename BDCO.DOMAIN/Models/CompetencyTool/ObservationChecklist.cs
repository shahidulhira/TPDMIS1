using BDCO.Core.Command;
using BDCO.Domain.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates
{
    [Table("ObservationChecklist")]
    public class ObservationChecklist : BaseModel
    {
        public long RecordID { get; set; }
        public string ChecklistId { get; set; }
        public string ProfileId { get; set; }
        public string Name { get; set; }
        public string ObserverName { get; set; }
        public string ObservationDate { get; set; }
        public string Q1_1_1 { get; set; }
        public string Q1_1_2 { get; set; }
        public string Q1_1_3 { get; set; }
        public string Q1_1_4 { get; set; }
        public string Q1_2_1 { get; set; }
        public string Q1_2_2 { get; set; }
        public string Q1_3_1 { get; set; }
        public string Q1_3_2 { get; set; }
        public string Q1_3_3 { get; set; }
        public string Q1_4_1 { get; set; }
        public string Q1_4_3 { get; set; }
        public string Q2_1_1 { get; set; }
        public string Q2_1_2 { get; set; }
        public string Q2_2_1 { get; set; }
        public string Q2_2_2 { get; set; }
        public string Q2_2_3 { get; set; }
        public string Q2_3_2 { get; set; }
        public string Q2_3_3 { get; set; }
        public string Q2_3_4 { get; set; }
        public string Q2_3_5 { get; set; }
        public string Q2_4_2 { get; set; }
        public string Q2_5_2 { get; set; }
        public string Q2_5_3 { get; set; }
        public string Q2_5_4 { get; set; }
        public string Q3_1_2 { get; set; }
        public string Q3_1_3 { get; set; }
        public string Q4_2_1 { get; set; }
        public string Q4_2_2 { get; set; }
        

        public List<ObservationChecklist> GetAll()
        {
            //List<InterviewQuestionnaire> result = unitOfWork.Repositories<InterviewQuestionnaire>().GetAll().ToList();
            //return result;
            return unitOfWork.Repositories<ObservationChecklist>().GetAll().ToList(); 
        }
        public List<ObservationChecklist> GetAll(string sql)
        {
            //List<InterviewQuestionnaire> result = new List<InterviewQuestionnaire>();
            //result = unitOfWork.GenericRepositories<InterviewQuestionnaire>().GetRecordSet(sql).ToList();
            //return result;
            return unitOfWork.GenericRepositories<ObservationChecklist>().GetRecordSet(sql).ToList();
        }
        public CommandResult Save(ObservationChecklist command)
        {
            try
            {
                string result = "";
                ObservationChecklist observationChecklist = new ObservationChecklist();
                Tools.CopyClass(observationChecklist, command);
                var IsExist = unitOfWork.GenericRepositories<ObservationChecklist>().FindBy(x => x.ProfileId == command.ProfileId).FirstOrDefault();
                if (IsExist == null)
                {
                    observationChecklist.Status = 1;
                    unitOfWork.GenericRepositories<ObservationChecklist>().Insert(observationChecklist);
                    result = unitOfWork.SaveChange();
                    LogEventStore(observationChecklist.RecordID.ToString(), "ObservationChecklist", "Save");
                }
                else
                {
                    observationChecklist.RecordID = IsExist.RecordID;
                    observationChecklist.Status = 1;

                    unitOfWork.GenericRepositories<ObservationChecklist>().Update(observationChecklist);
                    result = unitOfWork.SaveChange();
                    LogEventStore(observationChecklist.RecordID.ToString(), "ObservationChecklist", "Update");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = observationChecklist.RecordID,
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
        public CommandResult SaveOrUpdate(ObservationChecklist command)
        {
            try
            {
                long recordId = command.RecordID;
                long serverRecordID = command.RecordID;
                string result = "";
                ObservationChecklist observationChecklist = new ObservationChecklist();
                Tools.CopyClass(observationChecklist, command);
                var IsExist = unitOfWork.GenericRepositories<ObservationChecklist>().FindBy(x => x.ProfileId == command.ProfileId).FirstOrDefault();
                if (IsExist == null)
                {
                    observationChecklist.Status = 1;
                    unitOfWork.GenericRepositories<ObservationChecklist>().Insert(observationChecklist);
                    result = unitOfWork.SaveChange();
                    LogEventStore(observationChecklist.RecordID.ToString(), "ObservationChecklist", "Save");
                }
                else
                {
                    observationChecklist.RecordID = IsExist.RecordID;
                    observationChecklist.Status = 1;

                    unitOfWork.GenericRepositories<ObservationChecklist>().Update(observationChecklist);
                    result = unitOfWork.SaveChange();
                    LogEventStore(observationChecklist.RecordID.ToString(), "ObservationChecklist", "Update");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = observationChecklist.RecordID,
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

}
