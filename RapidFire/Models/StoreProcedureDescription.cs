using System;
using System.Collections.Generic;
using System.Text;

namespace RapidFireLib.Models
{
    public class StoreProcedureDescription
    {
        //Parameter Serial No
        public int ParamOrder { get; set; }
        public string ParameterName { get; set; }
        public string Type { get; set; }
        public int Length { get; set; }
    }
}
