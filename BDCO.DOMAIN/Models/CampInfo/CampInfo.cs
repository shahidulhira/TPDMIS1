using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BDCO.Domain.Models
{
    [Table("CampInfo")]
    public class CampInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CampId { get; set; }
        public string CampName { get; set; }
        public string CampNameBn { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public int? IsActive { get; set; }

        private UnitOfWork unitOfWork = new UnitOfWork();
        public List<CampInfo> GetCamp()
        {
            List<CampInfo> lst = new List<CampInfo>();
            lst = unitOfWork.GenericRepositories<CampInfo>().GetAll().ToList();
            return lst;
        }
    }
}
