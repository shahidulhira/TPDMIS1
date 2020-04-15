using RapidFireLib.Lib.Extension;
using RapidFireLib.Models;
using System.Collections.Generic;
using System.Linq;

namespace RapidFireLib.Lib.DB
{
    public class QueryDescriptor : IQueryDescriptor
    {
        private readonly string sqlQueryForReadStoreProcedureParameters = @"SELECT  
                                               PARAMETER_ID ParamOrder,
                                               Name ParameterName,  
                                               TYPE_NAME(USER_TYPE_ID) Type,  
                                               MAX_LENGTH Length   
                                             FROM SYS.PARAMETERS WHERE OBJECT_ID = OBJECT_ID('####STOREPROCEDURENAME####')
                                             ORDER BY PARAMETER_ID";
        public string GetQueryString(string spName) => sqlQueryForReadStoreProcedureParameters.Replace("####STOREPROCEDURENAME####",spName);
        public string BuildQuery(List<StoreProcedureDescription> storeProcedureDescriptions, object storeProcedureValues, string spName)
        {
            string queryString = $"EXEC {spName} ";
            //For Dictionary Type Data Query Building
            if (storeProcedureValues.GetType() == typeof(Dictionary<string,string>))
            {
                var castedObject = (Dictionary<string, string>)storeProcedureValues;
                foreach (StoreProcedureDescription storeProcedureDescription in storeProcedureDescriptions.OrderBy(x => x.ParamOrder))
                    queryString += $"'{castedObject[storeProcedureDescription.ParameterName.Replace("@", "")].ToString()}' ,";
            }
            //For Model Type Data Query Building
            else
                foreach (StoreProcedureDescription storeProcedureDescription in storeProcedureDescriptions.OrderBy(x => x.ParamOrder))
                    queryString += $"'{storeProcedureValues.GetPropertyValue(storeProcedureDescription.ParameterName.Replace("@", "")).ToString()}' ,";
            return queryString.Trim(',').Trim(' ');
        }
    }
}
