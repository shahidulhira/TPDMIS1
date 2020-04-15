using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates
{
    [Table("ReferralHospital")]
    public class ReferralHospital
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordId { get; set; }
        public int HospitalType { get; set; }
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        private UnitOfWork unitOfWork = new UnitOfWork();
        public List<ReferralHospital> GetReferralInstitute(int hospitalType)
        {
            List<ReferralHospital> lst = new List<ReferralHospital>();
            lst = unitOfWork.GenericRepositories<ReferralHospital>().FindBy(h => h.HospitalType == hospitalType).ToList();
            return lst;
        }
    }

}
