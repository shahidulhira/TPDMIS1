using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidFireLib.Models
{
    public class ModelAccess
    {
        public string ModelName { get; set; }
        public bool IsRead { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
}
