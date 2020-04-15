using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates
{
    public class BaseViewModel
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? DataCollectionDate { get; set; }
        public int? IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public string VerifierName { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string VerificationNote { get; set; }   
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? TotalRecord { get; set; }
        public string Caption { get; set; }
    }
}
