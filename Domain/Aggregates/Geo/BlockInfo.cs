using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Aggregates
{
    public class BlockInfo
    {
        [Key]
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public string BlockFullName { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public int CampId { get; set; }
        public int CenterId { get; set; }
    }
}
