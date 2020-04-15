using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BDCO.Domain.Aggregates
{
    [Table("GenderInfo")]
    public class GenderInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Gender { get; set; }
        public string GenderName { get; set; }
        public bool? IsActive { get; set; }

        private UnitOfWork unitOfWork = new UnitOfWork();
        public List<GenderInfo> GetGenderInfo()
        {
            List<GenderInfo> lst = new List<GenderInfo>();
            lst = unitOfWork.GenericRepositories<GenderInfo>().GetAll().ToList();
            return lst;
        }
    }
}
