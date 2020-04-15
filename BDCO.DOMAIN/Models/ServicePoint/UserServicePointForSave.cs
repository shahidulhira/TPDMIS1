using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates
{
    public class UserServicePointForSave
    {
        public List<SelectedServicePoint> lstRcN { get; set; }
        public int UserId { get; set; }
    }
    public class SelectedServicePoint
    {
        public string ServicePointId { get; set; }
    }
}
