using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BDCO.Domain.Aggregates
{
    [Table("ObservationalOrganization")]
    public class ObservationalOrganization : AggregateRoot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }

        private UnitOfWork _unitOfWork = new UnitOfWork();
        public List<ObservationalOrganization> GetaObservationalOrganizationList()
        {
            var result = _unitOfWork.GenericRepositories<ObservationalOrganization>().GetAll().ToList();
            return result;
        }
    }
}
