using BDCO.Core.Command;
using BDCO.Domain.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BDCO.Domain.Aggregates
{
    [Table("UniqueId")]
    public class UniqueId : AggregateRoot
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordId { get; set; }
        public long? ServerRecordId { get; set; }
        [JsonIgnore]
        public int? UserId { get; set; }
        public string BenId { get; set; }
        public int? Status { get; set; }

        private UnitOfWork unitOfWork = new UnitOfWork();
        public List<UniqueId> SaveAndGet(RequestForUniqueId requestForUniqueId,int? userId)
        {
            return unitOfWork.RawSqlQuery<UniqueId>($"exec APIGetUniqueId '{userId}','{400-requestForUniqueId.UnusedBenId??400}'");
        }
        public CommandResult Save(List<UniqueId> command,int? userId)
        {
            List<UniqueId> uniqueIds = new List<UniqueId>();
            command.ForEach(x => {
                if (x.ServerRecordId == 0 || x.ServerRecordId == null)
                {
                    UniqueId uniqueId = new UniqueId();
                    x.UserId = userId;
                    Tools.CopyClass(uniqueId, x);
                    unitOfWork.GenericRepositories<UniqueId>().Insert(uniqueId);
                    LogEventStore(uniqueId.RecordId.ToString(), "UniqueId", "Update");
                }
                else
                {
                    UniqueId uniqueIdForUpdate = new UniqueId();
                    uniqueIdForUpdate = unitOfWork.GenericRepositories<UniqueId>().FindBy(c => c.RecordId == x.ServerRecordId).FirstOrDefault();
                    uniqueIdForUpdate.Status = x.Status;
                    uniqueIdForUpdate.UserId = userId;
                    uniqueIdForUpdate.BenId = x.BenId;
                    uniqueIdForUpdate.ServerRecordId = x.RecordId;
                    uniqueIds.Add(uniqueIdForUpdate);
                    unitOfWork.GenericRepositories<UniqueId>().Update(uniqueIdForUpdate,false);
                    LogEventStore(uniqueIdForUpdate.RecordId.ToString(), "UniqueId", "Update");
                }
            });
            string result = unitOfWork.SaveChange();
            return new CommandResult()
            {
                Success = result != "" ? false : true,
                Status = result != "" ? 400 : 200,
                Message = result != "" ? result : "Record Saved successfully."
            }; ;
        }
    }
    public class RequestForUniqueId
    {

        public int? UserId { get; set; }
        public int? UnusedBenId { get; set;}
    }
}
