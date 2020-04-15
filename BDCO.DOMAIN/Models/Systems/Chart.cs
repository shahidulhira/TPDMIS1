using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Models.Systems
{
    public class ChartData
    {
        public List<string> Categories { get; set; }
        public List<SeriesObjects> Series { get; set; }
    }
    public class SeriesObjects
    {
        public string name { get; set; }
        public List<string> data { get; set; }
    }
    public class GmpTrandView
    {

    }
    
}
