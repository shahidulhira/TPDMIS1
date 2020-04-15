using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDCO.Web.Models
{
    public class ResponseReturn<T>
    {
        public ResponseReturn()
        {
            FetchPacket = new FetchPacket<T>();
        }
        public bool Success { get; set; }
        public int Status { get; set; }
        public String Message { get; set; }
        public long ServerRecordId { get; set; }
        public long RecordId { get; set; }
        public String BenID { get; set; }
        public String UID { get; set; }
        public FetchPacket<T> FetchPacket { get; set; }
    }

    public class ResponseReturn
    {
        public String ID { get; set; }
    }
}