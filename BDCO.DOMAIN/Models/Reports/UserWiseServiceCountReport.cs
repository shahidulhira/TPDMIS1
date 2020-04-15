using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Models.Reports
{
    public class UserWiseServiceCountReportView
    {
        public int? RecordId { get; set; }
        public long RowNo { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int Member { get; set; }
        public int Discharge { get; set; }
        public int Distribution { get; set; }
        public int Session { get; set; }
        public int Gmp { get; set; }
        public int? TotalRecord { get; set; }
    }
}
