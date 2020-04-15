using System;

namespace RapidFireLib.Models.Api
{
    public class CommandResult
    {
        public CommandResult()
        {
            Success = true;
        }

        public bool Success { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public long ServerRecordId { get; set; }
        public long RecordId { get; set; }

    }
}
