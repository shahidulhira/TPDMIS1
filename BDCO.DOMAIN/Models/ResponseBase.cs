using BDCO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates
{
    //This class will Reponse Item with small line of codes
    public static class ResponseBase
    {
        /// <summary>
        /// Command Result Success Result With Simplification
        /// </summary>
        /// <param name="result"></param>
        /// <param name="recordId"></param>
        /// <param name="serverRecordId"></param>
        /// <returns></returns>
        public static CommandResult CommandResultSuccess(string result,long recordId,long serverRecordId)
        {
            return new CommandResult()
            {
                Success = result != "" ? false : true,
                Status = result != "" ? 400 : 200,
                Message = result != "" ? result : "Record Saved successfully.",
                ServerRecordId = serverRecordId,
                RecordId = recordId
            };
        }
        /// <summary>
        /// Command Result Error Result With Simplification
        /// </summary>
        /// <param name="message"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static CommandResult CommandResultError(string message,long recordId)
        {
            return new CommandResult()
            {
                Success = false,
                Status = 400,
                Message = message,
                RecordId = recordId
            };
        }
    }
}
