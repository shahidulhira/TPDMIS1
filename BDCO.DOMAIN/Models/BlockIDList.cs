
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Models.MemberInfo
{
    public class BlockIDList
    {
        public string BlockName { get; set; }

        private UnitOfWork unitOfWork = new UnitOfWork();

        public List<BlockIDList> GetBlockIDList(BlockRequestParams query)
        {
            try
            {
                string sql = string.Format("EXEC BlockIDList '{0}','{1}','{2}','{3}'", query.DistrictCode, query.UpazilaCode, query.UnionCode, query.VillageCode);
                List<BlockIDList> result = unitOfWork.GenericRepositories<BlockIDList>().GetRecordSet(sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
