using RapidFireLib.Lib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidFireLib.Lib.Core
{
    public static class am 
    {
        public static am_db db = new am_db();
    }
    public class am_db
    {
        public string Success = "Record Saved Successfully!";
        public string Successful = "Successful";
        public string NoPermission = "You do not have permission to add new record!";
    }
}
