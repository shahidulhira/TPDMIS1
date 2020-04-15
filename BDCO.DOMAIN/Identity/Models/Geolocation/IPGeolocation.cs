using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates
{
    public class IPGeolocation : AggregateRoot
    {

        public int ID { get; set; }
        public int IPCode { get; set; }
        public string IPName { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }

    }
}
