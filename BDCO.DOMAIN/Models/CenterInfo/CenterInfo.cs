using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Models
{
    public class CenterInfo
    {

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]       
        public int CenterId { get; set; }
        public string CenterName { get; set; }
        public string CenterNameBn { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public int? IsActive { get; set; }

        private UnitOfWork _unitOfWork = new UnitOfWork();
        public List<CenterInfo> GetAllCenter()
        {
            List<CenterInfo> res = new List<CenterInfo>();
            string sqlQuery = string.Format("SELECT * FROM CenterInfo");
            res = _unitOfWork.GenericRepositories<CenterInfo>().GetRecordSet(sqlQuery).ToList();
            return res;
        }
    }
}
