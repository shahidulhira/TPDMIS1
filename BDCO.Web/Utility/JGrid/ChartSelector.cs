using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDCO.Web.Utility.JGrid
{
    public class ChartSelector
    {
    
            string sql = "";
            public string getSelector(JGridRequest packet)
            {
                FilterData filter = new FilterData();
                if (packet.Data != null && packet.RequestType != "Update")
                    filter = JsonConvert.DeserializeObject<FilterData>(packet.Data.ToString());
                switch (packet.ModelName)
                {
                    case "GmpTrandView":
                        sql = string.Format(@"EXEC GetGmpTrand '{0}'",packet.RecordId);
                        break;                    
                }
                return sql;
            }
        }
    }
