using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Web.Utility.JGrid
{
    public class JGridRequest
    {
        public string ControllerName { get; set; }
        public string ModelName { get; set; }
        public string RequestType { get; set; }
        public string RecordType { get; set; }
        public string RecordId { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public string Data { get; set; }
        public bool IsOverride { get; set; }

    }
}
