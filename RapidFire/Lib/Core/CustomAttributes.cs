using System;
using System.Collections.Generic;
using System.Text;

namespace RapidFireLib.Lib.Core
{
    public class ContextAttribute : Attribute
    {
        public string DbContextName { get; set; }
        public ContextAttribute(string _DbContextName)
        {
            DbContextName = _DbContextName;
        }
    }

    public class AuditDataView : Attribute
    {
        public AuditDataView(string modelName)
        {
            // logic to save in audirt table ??
            string d = modelName;
        }
    }
}
