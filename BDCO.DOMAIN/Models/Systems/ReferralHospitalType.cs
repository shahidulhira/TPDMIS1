using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Aggregates
{
    [Table("ReferralHospitalType")]
    public class ReferralHospitalType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordId { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }

        private UnitOfWork unitOfWork = new UnitOfWork();
        public List<ReferralHospitalType> GetReferralHospital()
        {
            List<ReferralHospitalType> lst = new List<ReferralHospitalType>();
            lst = unitOfWork.GenericRepositories<ReferralHospitalType>().GetAll().ToList();
            return lst;
        }
    }
}
