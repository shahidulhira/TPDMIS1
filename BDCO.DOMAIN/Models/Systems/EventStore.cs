using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates
{
    [Table("AppEventStore")]
    public class EventStore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditLogID { get; set; }
        public string ReffrenceNo { get; set; }
        public string AggregateName { get; set; }
        public string OperationType { get; set; }
        public DateTime LogDateTime { get; set; }
        public string UserID { get; set; }
    }
}
