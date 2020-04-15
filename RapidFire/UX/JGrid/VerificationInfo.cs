using System;
using System.Collections.Generic;
using System.Text;

namespace RapidFireLib.UX.JGrid
{
    public class VerificationInfo
    {
        public string UserName { get; set; }
        public DateTime? DataCollectionDate { get; set; }
        public int? IsVerified { get; set; }
        public string VerifierName { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string VerificationNote { get; set; }
    }
}
