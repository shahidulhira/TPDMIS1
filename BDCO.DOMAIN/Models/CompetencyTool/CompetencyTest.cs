using BDCO.Core.Command;
using BDCO.Domain.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;

namespace BDCO.Domain.Aggregates
{
    [Table("CompetencyTest")]
    public class CompetencyTest : BaseModel
    {
        public long RecordID { get; set; }
        [Key]
        public string CompetencyTestId { get; set; }
        public string ProfileId { get; set; }
        public string ExamTestId { get; set; }
        public string ExaminerId { get; set; }
        public string FacilityId { get; set; }
        public string TeachingLevel { get; set; }
        public string PartnerId { get; set; }
        public string LOneEnglish { get; set; }
        public string LTwoEnglish { get; set; }
        public string LThreeEnglish { get; set; }
        public string LFourEnglish { get; set; }
        public string LOneBurmese { get; set; }
        public string LTwoBurmese { get; set; }
        public string LThreeBurmese { get; set; }
        public string LFourBurmese { get; set; }
        public string LOneScience { get; set; }
        public string LTwoScience { get; set; }
        public string LThreeScience { get; set; }
        public string LFourScience { get; set; }
        public string LOneMath { get; set; }
        public string LTwoMath { get; set; }
        public string LThreeMath { get; set; }
        public string LFourMath { get; set; }
        public string LOneLifeSkills { get; set; }
        public string LTwoLifeSkills { get; set; }
        public string LThreeLifeSkills { get; set; }
        public string LFourLifeSkills { get; set; }


        public List<CompetencyTest> GetAll()
        {
            List<CompetencyTest> result = unitOfWork.Repositories<CompetencyTest>().GetAll().ToList();
            return result;
        }
        public List<CompetencyTest> GetAll(string sql)
        {
            //List<CompetencyTest> result = new List<CompetencyTest>();
            //result = unitOfWork.GenericRepositories<CompetencyTest>().GetRecordSet(sql).ToList();
            //return result;

            return unitOfWork.GenericRepositories<CompetencyTest>().GetRecordSet(sql).ToList();
        }
        public CommandResult Save(CompetencyTest command)
        {
            try
            {
                string result = "";
                CompetencyTest competencyTest = new CompetencyTest();
                Tools.CopyClass(competencyTest, command);
                var IsExist = unitOfWork.GenericRepositories<CompetencyTest>().FindBy(x => x.ProfileId == command.ProfileId).FirstOrDefault();
                if (IsExist == null)
                {
                    competencyTest.Status = 1;
                    unitOfWork.GenericRepositories<CompetencyTest>().Insert(competencyTest);
                    result = unitOfWork.SaveChange();
                    LogEventStore(competencyTest.RecordID.ToString(), "CompetencyTest", "Save");
                }
                else
                {
                    competencyTest.RecordID = IsExist.RecordID;
                    competencyTest.Status = 1;

                    unitOfWork.GenericRepositories<CompetencyTest>().Update(competencyTest);
                    result = unitOfWork.SaveChange();
                    LogEventStore(competencyTest.RecordID.ToString(), "CompetencyTest", "Update");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = competencyTest.RecordID,
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
        public CommandResult SaveOrUpdate(CompetencyTest command)
        {
            try
            {
                long recordId = command.RecordID;
                long serverRecordID = command.RecordID;
                string result = "";
                CompetencyTest competencyTest = new CompetencyTest();
                Tools.CopyClass(competencyTest, command);
                var IsExist = unitOfWork.GenericRepositories<CompetencyTest>().FindBy(x => x.ProfileId == command.ProfileId).FirstOrDefault();
                if (IsExist == null)
                {
                    competencyTest.Status = 1;
                    unitOfWork.GenericRepositories<CompetencyTest>().Insert(competencyTest);
                    result = unitOfWork.SaveChange();
                    LogEventStore(competencyTest.RecordID.ToString(), "CompetencyTest", "Save");
                }
                else
                {
                    competencyTest.RecordID = IsExist.RecordID;
                    competencyTest.Status = 1;

                    unitOfWork.GenericRepositories<CompetencyTest>().Update(competencyTest);
                    result = unitOfWork.SaveChange();
                    LogEventStore(competencyTest.RecordID.ToString(), "CompetencyTest", "Update");
                }
                return new CommandResult()
                {
                    Success = result != "" ? false : true,
                    Status = result != "" ? 400 : 200,
                    Message = result != "" ? result : "Record Saved successfully.",
                    ServerRecordId = competencyTest.RecordID,
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
        public bool Update(CompetencyUpdate competency)
        {
            string sql = $@"EXEC spUpdateCompetencyTest {competency.ProfileId},{competency.CompetencyTestId},{competency.ExamTestId},
                        { competency.LOneEnglish},{competency.LTwoEnglish},{competency.LThreeEnglish},{competency.LFourEnglish},
                        {competency.LOneBurmese},{competency.LTwoBurmese},{competency.LThreeBurmese},{competency.LFourBurmese},   
                        {competency.LOneScience},{competency.LTwoScience},{competency.LThreeScience},{competency.LFourScience},
                        {competency.LOneMath},{competency.LTwoMath},{competency.LThreeMath},{competency.LFourMath},
                        {competency.LOneLifeSkills},{competency.LTwoLifeSkills},{competency.LThreeLifeSkills},{competency.LFourLifeSkills},{competency.User}";
            
            SqlConnection connection = new SqlConnection("Data Source=10.12.1.2;Initial Catalog=TPDMIS;user id=sa; password=bdco");
            connection.Open();
            SqlCommand cmd = new SqlCommand(sql, connection);
            if(cmd.ExecuteNonQuery()>0)
            {
                connection.Close();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class CompetencyTestView
    {
        public string CompetencyTestId { get; set; }
        public string ProfileId { get; set; }
        public string ExamTestId { get; set; }
        public string ExaminerId { get; set; }
        public string FacilityId { get; set; }
        public string TeachingLevel { get; set; }
        public string PartnerId { get; set; }
        public string LOneEnglish { get; set; }
        public string LTwoEnglish { get; set; }
        public string LThreeEnglish { get; set; }
        public string LFourEnglish { get; set; }
        public string LOneBurmese { get; set; }
        public string LTwoBurmese { get; set; }
        public string LThreeBurmese { get; set; }
        public string LFourBurmese { get; set; }
        public string LOneScience { get; set; }
        public string LTwoScience { get; set; }
        public string LThreeScience { get; set; }
        public string LFourScience { get; set; }
        public string LOneMath { get; set; }
        public string LTwoMath { get; set; }
        public string LThreeMath { get; set; }
        public string LFourMath { get; set; }
        public string LOneLifeSkills { get; set; }
        public string LTwoLifeSkills { get; set; }
        public string LThreeLifeSkills { get; set; }
        public string LFourLifeSkills { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string DataCollectionDate { get; set; }
        public string DataCollectionTime { get; set; }
        public int? User { get; set; }
        public string DisplayName { get; set; }
        public int? Status { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModificationDate { get; set; }
        public int? IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public string VerificationDate { get; set; }
        public string VerificationNote { get; set; }
    }
    public class CompetencyTestResultsVw
    {
        public string ProfileId { get; set; }
        public string TeachingLevel { get; set; }
        public string English { get; set; }
        public string Burmese { get; set; }
        public string Science { get; set; }
        public string Math { get; set; }
        public string LifeSkills { get; set; }
    }
    public class CompetencyUpdate
    {
        public string CompetencyTestId { get; set; }
        public string ProfileId { get; set; }
        public string ExamTestId { get; set; }
        public string LOneEnglish { get; set; }
        public string LTwoEnglish { get; set; }
        public string LThreeEnglish { get; set; }
        public string LFourEnglish { get; set; }
        public string LOneBurmese { get; set; }
        public string LTwoBurmese { get; set; }
        public string LThreeBurmese { get; set; }
        public string LFourBurmese { get; set; }
        public string LOneScience { get; set; }
        public string LTwoScience { get; set; }
        public string LThreeScience { get; set; }
        public string LFourScience { get; set; }
        public string LOneMath { get; set; }
        public string LTwoMath { get; set; }
        public string LThreeMath { get; set; }
        public string LFourMath { get; set; }
        public string LOneLifeSkills { get; set; }
        public string LTwoLifeSkills { get; set; }
        public string LThreeLifeSkills { get; set; }
        public string LFourLifeSkills { get; set; }
        public int? User { get; set; }
    }
}
