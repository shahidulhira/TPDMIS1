using Microsoft.EntityFrameworkCore;
using RapidFireLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RapidFireLib.Lib.DB
{
    public interface IQueryDescriptor
    {
        string GetQueryString(string spName);
        string BuildQuery(List<StoreProcedureDescription> storeProcedureDescriptions, object storeProcedureValues, string spName);
    }
}
