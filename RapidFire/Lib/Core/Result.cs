using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidFireLib.Lib.Core
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public Object Model { get; set; }
        public Exception Exception { get; set; }
        public Result()
        {

        }
        public Result(bool Success, string Message, int Status = 0, Object model = null, Exception ex = null)
        {
            this.Success = Success;
            this.Message = Message;
            this.Status = Status;
            this.Model = model;
            this.Exception = ex;
        }

        public Result Set(bool Success, string Message, int Status = 0, Object model = null, Exception ex = null)
        {
            this.Success = Success;
            this.Message = Message;
            this.Status = Status;
            this.Model = model;
            this.Exception = ex;
            return this;
        }
    }
}
