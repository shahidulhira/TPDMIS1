using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Models
{
    [Table("AppResource")]
    public class Resources
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PageTitle { get; set; }
        public bool Active { get; set; }
        private UnitOfWork unitOfWork = new UnitOfWork();

        public List<Resources> GetResources()
        {
            try
            {
                string sql = @"SELECT * FROM AppResource Where Active=1 ORDER BY Name";
                
                List<Resources> result = unitOfWork.Repositories<Resources>().GetRecordSet(sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
