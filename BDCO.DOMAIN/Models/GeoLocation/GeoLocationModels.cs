using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates.GeoLocation
{
    public class PermittedGeoLocation
    {
        public List<District> District { get; set; }
        public List<Upazila> Upazila { get; set; }
        public List<Union> Unions { get; set; }
        public List<Village> Village { get; set; }
        public long TotalRecord { get; set; }
    }
}
