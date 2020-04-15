using BDCO.Domain.Aggregates;
using BDCO.Domain.Query;
using BDCO.Domain.RequestParams;
using BDCO.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace BDCO.Domain.Aggregates
{
    [Table("ServicePoint")]
    public class ServicePoint : AggregateRoot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ServicePointId { get; set; }
        public string ServicePointName { get; set; }
        public string ServicePointNameBangla { get; set; }
        public string UserType { get; set; }
        public int? IsActive { get; set; }


        private UnitOfWork unitOfWork = new UnitOfWork();
        public List<ServicePoint> GetServicePoint()
        {
            List<ServicePoint> lst = new List<ServicePoint>();
            lst = unitOfWork.GenericRepositories<ServicePoint>().GetAll().ToList();
            return lst;
        }
        public string ServicePointInfo
        {
            get
            {
                return ServicePointId + "-" + ServicePointName;
            }
        }
        public string ServicePointInfoBangla
        {
            get
            {
                return ServicePointId + "-" + ServicePointNameBangla;
            }
        }
        // public string UserType { get; set; }

        public List<ServicePoint> GetServicePointList()
        {
            return unitOfWork.GenericRepositories<ServicePoint>().GetAll().ToList() ;
        }
        public List<ServicePoint> GetServicePointListForDropdownList(ServicePointView query)
        {
            
            return unitOfWork.GenericRepositories<ServicePoint>().GetAll().OrderBy(d => d.ServicePointId).ToList();
        }
        public List<ServicePoint> GetServicePointListForPermission(ServicePointForPermission query)
        {
            
            var result = unitOfWork.GenericRepositories<ServicePoint>().GetAll().Where(s => s.UserType == query.UserType).ToList();
            return result;
        }
    }
}
